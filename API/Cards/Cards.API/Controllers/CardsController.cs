using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System; 
using System.Threading.Tasks;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        public CardsController(CardsDbContext context)
        {
            this.context = context;
        }
        private readonly CardsDbContext context;
        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards=  await context.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get single card
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute]Guid id)
        {
            var card= await context.Cards.FirstOrDefaultAsync(x=>x.Id==id);
            if (card==null)
            {
                return NotFound();
            }
            return Ok(card);
        }
        
        //Add card
        [HttpPost]
        public async Task<IActionResult> AddCard(Card card)
        {
            card.Id = Guid.NewGuid();
            context.Cards.Add(card);
            await context.SaveChangesAsync(); 
            return CreatedAtAction(nameof(GetCard),new {id= card.Id}, card );
        }

        //Updating A Card
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute]Guid id,[FromBody]Card card)
        {
            var existingCard = await context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard==null)
            {
                return NotFound();
            }
            existingCard.CardholderName = card.CardholderName;
            existingCard.CardNumber = card.CardNumber;
            existingCard.ExpireMonth = card.ExpireMonth;
            existingCard.ExpiryYear = card.ExpiryYear;
            existingCard.CVC = card.CVC;
            await context.SaveChangesAsync();
            return Ok(existingCard);
        }

        [HttpDelete]
        [Route("{id:Guid}")] 
        public async Task<IActionResult> DeleteCard([FromRoute]Guid id)
        {
            var existingCard = await context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard == null)
            {
                return NotFound();
            }
            context.Remove(existingCard);
            context.SaveChanges();

            return Ok(existingCard);
        }
    }
}

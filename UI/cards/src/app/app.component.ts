import { Component, OnInit } from '@angular/core';
import { Card } from './models/card.model';
import { CardsService } from './service/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  title = 'cards';
  cards:Card[]=[];
  card:Card={
    id:'',
    cardNumber:'',
    cardholderName:'',
    expiryMonth:'',
    expiryYear:'',
    cvc:''
  }
 
  constructor(private cardsService:CardsService) {}

  ngOnInit(): void {
    this.getAllCards();
  }

  getAllCards(){
    this.cardsService.getAllCards()
    .subscribe(
      response=>{
        this.cards=response;
      }
    );
  }

  onSubmit(){

    if(this.card.id===''){
      this.cardsService.addCard(this.card)
      .subscribe(res=>{
        this.getAllCards();
        this.card={
          id:'',
          cardNumber:'',
          cardholderName:'',
          expiryMonth:'',
          expiryYear:'',
          cvc:''
        }
      });
    }else{ //if the id exist we use update
      this.updateCard(this.card);
    }
   
  }

  clearFills(card : Card){

  }

  deleteCard(id:string){
    this.cardsService.deleteCard(id)
    .subscribe(res=>{
      this.getAllCards();
    });
  }

  populateForm(card : Card){
    this.card=card;
  }

  updateCard(card:Card){
    this.cardsService.updateCard(card)
    .subscribe(res=>{
      this.getAllCards();
    });
  }

}

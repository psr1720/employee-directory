import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  homeWrapperClass = "home-wrapper";
  makeHomeWrapperActive(){
    if(this.homeWrapperClass == "home-wrapper"){
      this.homeWrapperClass = "home-wrapper active";
    }
    else{
      this.homeWrapperClass = "home-wrapper";
    }
  }
}

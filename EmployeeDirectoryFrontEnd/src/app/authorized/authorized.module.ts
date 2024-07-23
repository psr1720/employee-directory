import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorizedRoutingModule } from './authorized-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    AuthorizedRoutingModule,
    SharedModule
  ]
})
export class AuthorizedModule { }

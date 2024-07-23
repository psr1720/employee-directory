import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeRoutingModule } from './employee-routing.module';
import { AuthorizedModule } from '../authorized.module';
import { ReactiveFormsModule } from '@angular/forms'
import { AddEmployeePageComponent } from './components/add-employee-page/add-employee-page.component';
import { EmployeeCardPageComponent } from './components/employee-card-page/employee-card-page.component';
import { EmployeeChangePasswordPageComponent } from './components/employee-change-password-page/employee-change-password-page.component';


@NgModule({
  declarations: [
    AddEmployeePageComponent,
    EmployeeCardPageComponent,
    EmployeeChangePasswordPageComponent,
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    AuthorizedModule,
    ReactiveFormsModule
  ]
})
export class EmployeeModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeePageComponent } from './components/employee-page/employee-page.component';
import { SharedModule } from "../../shared/shared.module";
import { AddEmployeePageComponent } from './components/add-employee-page/add-employee-page.component';
import { EmployeeCardPageComponent } from './components/employee-card-page/employee-card-page.component';
import { EmployeeChangePasswordPageComponent } from './components/employee-change-password-page/employee-change-password-page.component';

const routes: Routes = [
  {
    path:'', 
    component:EmployeePageComponent,
  },
  {
    path:'add',
    component:AddEmployeePageComponent,
  },
  {
    path:'cards/:id',
    component:EmployeeCardPageComponent,
  },
  {
    path:'changepassword',
    component: EmployeeChangePasswordPageComponent
  }

];

@NgModule({
    declarations: [EmployeePageComponent],
    exports: [RouterModule],
    imports: [RouterModule.forChild(routes), SharedModule]
})
export class EmployeeRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RolePageComponent } from './components/role-page/role-page.component';
import { AddRolePageComponent } from './components/add-role-page/add-role-page.component';

const routes: Routes = [
  {
    path: '',
    component:RolePageComponent
  },
  {
    path: 'add',
    component:AddRolePageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoleRoutingModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule } from './role-routing.module';
import { RolePageComponent } from './components/role-page/role-page.component';
import { AddRolePageComponent } from './components/add-role-page/add-role-page.component';
import { SharedModule } from '../../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    RolePageComponent,
    AddRolePageComponent,
  ],
  imports: [
    CommonModule,
    RoleRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class RoleModule { }

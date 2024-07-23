import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EmployeePageComponent } from './employee/components/employee-page/employee-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        redirectTo: 'employee',
        pathMatch: 'full'
      },
      {
        path: 'employee',
        loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule),
      },
      {
        path: 'role',
        loadChildren: () => import('./role/role.module').then(m => m.RoleModule),
      }
    ]
  },
]

// children: [
//   // {
//   //   path: '',
//   //   component: HomeComponent
//   // },
//   // {
//   //   path: "home/employee", loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule)
//   // }
// ],

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizedRoutingModule { }

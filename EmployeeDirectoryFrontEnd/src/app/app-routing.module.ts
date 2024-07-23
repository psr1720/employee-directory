import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './unauthorized/components/login/login.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'unauth',
    pathMatch: 'full'
  },
  {
    path: "auth",
    loadChildren: () => import('./authorized/authorized.module').then(m => m.AuthorizedModule)
  },
  {
    path: "unauth",
    loadChildren: () => import('./unauthorized/unauthorized.module').then(m => m.UnauthorizedModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes),],
  exports: [RouterModule]
})
export class AppRoutingModule { }

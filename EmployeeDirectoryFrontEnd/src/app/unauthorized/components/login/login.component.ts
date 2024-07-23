import { Component } from '@angular/core';
import { LoginService } from '../../services/login/login.service';
import { Login } from '../../models/Login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private _loginService: LoginService, private router: Router) { }

  userName: string = 'TZ0001';
  password: string = 'Password';
  errorMessage: string = 'Wrong Credentials! Try again';
  showError: boolean = false;
  loginUser() {
    let login: Login = {
      userId: this.userName,
      password: this.password
    }
    this._loginService.loginUser(login).subscribe({
      next: (response: any) => {
        if (response.isSuccess) {
          localStorage.setItem("AuthToken", response.data)
          this.showError = false
          this.router.navigate(['/auth']);
        }
        else {
          this.showError = true;
        }
      },
      error: (error: any) => {
        this.showError = true;
      }
    })
  }
}

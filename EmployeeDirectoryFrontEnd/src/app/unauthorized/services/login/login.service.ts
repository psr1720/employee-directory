import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../../../shared/models/Response';
import { Login } from '../../models/Login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private http: HttpClient) { }

  loginUser(login: Login): Observable<Response<string>> {
    return this.http.post<Response<string>>('https://localhost:7072/api/Employee/Login', login);
  }
}

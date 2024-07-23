import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/Pagination';
import { Department } from '../../models/Department';
import { Response } from '../../models/Response';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private apiUrl = 'https://localhost:7072'

  constructor(private http:HttpClient) { }

  getData(page:number,limit:number):Observable<Response<Pagination<Department[]>>>{
    return this.http.get<Response<Pagination<Department[]>>>(`${this.apiUrl}/api/Department/?page=${page}&limit=${limit}`)
  }
}
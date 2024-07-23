import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/Pagination';
import { Project } from '../../models/Project';
import { Response } from '../../models/Response';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private apiUrl = 'https://localhost:7072'

  constructor(private http: HttpClient) {}

  getData(page:number,limit:number):Observable<Response<Pagination<Project[]>>>{
    return this.http.get<Response<Pagination<Project[]>>>(`${this.apiUrl}/api/Project/?page=${page}&limit=${limit}`)
  }
}


import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/Pagination';
import { Role } from '../../models/Role';
import { Response } from '../../models/Response';
import { Filters } from '../../models/Filters';
import { RoleRequest } from '../../models/RoleRequest';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private apiUrl = 'https://localhost:7072/api/Role'

  constructor(private http: HttpClient) {}

  getData(page:number,limit:number, filters?:Filters):Observable<Response<Pagination<Role[]>>>{
    return this.http.post<Response<Pagination<Role[]>>>(`${this.apiUrl}/GetAll?page=${page}&limit=${limit}`,filters)
  }
  getDataById(id: number):Observable<Response<Role>>{
    return this.http.get<Response<Role>>(`${this.apiUrl}/${id}`)
  }
  postData(data: RoleRequest):Observable<Response<RoleRequest>>{
    return this.http.post<Response<RoleRequest>>(`${this.apiUrl}`,data)
  }
  updateData(id: number, data: RoleRequest):Observable<Response<Role>>{
    return this.http.put<Response<Role>>(`${this.apiUrl}/${id}`,data)
  }
}
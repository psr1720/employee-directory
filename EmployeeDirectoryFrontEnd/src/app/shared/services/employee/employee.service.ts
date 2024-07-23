import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Filters } from '../../models/Filters';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/Pagination';
import { Employee } from '../../models/Employee';
import { Response } from '../../models/Response';
import { EmployeeRequest } from '../../models/EmployeeRequest';
import { ChangePassword } from '../../models/ChangePassword';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7072/api/Employee'

  constructor(private http: HttpClient) { }

  getData(page: number, limit: number, filters?: Filters): Observable<Response<Pagination<Employee[]>>> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    return this.http.post<Response<Pagination<Employee[]>>>(`${this.apiUrl}/GetAll?page=${page}&limit=${limit}`, filters);
  }

  getDataByID(employeeId: string): Observable<Response<Employee>> {
    return this.http.get<Response<Employee>>(`${this.apiUrl}/${employeeId}`);
  }

  getDataByRoleID(roleID: number): Observable<Response<Pagination<Employee[]>>> {
    return this.http.get<Response<Pagination<Employee[]>>>(`${this.apiUrl}/role/${roleID}`);
  }

  postData(data: EmployeeRequest): Observable<Response<EmployeeRequest>> {
    return this.http.post<Response<EmployeeRequest>>(`${this.apiUrl}/`, data);
  }

  updateData(id: number, data: EmployeeRequest): Observable<Response<EmployeeRequest>> {
    return this.http.put<Response<EmployeeRequest>>(`${this.apiUrl}/${id}`, data);
  }
  
  deleteData(id: string): Observable<Response<boolean>> {
    return this.http.delete<Response<boolean>>(`${this.apiUrl}?id=${id}`);
  }

  updateJobTitleId(id: number, jobTitleID: number):Observable<Response<EmployeeRequest>>{
    return this.http.patch<Response<EmployeeRequest>>(`${this.apiUrl}/emp/${id}/jobTitle/${jobTitleID}`,{})
  }
  changePassword(changePassword: ChangePassword){
    return this.http.post<Response<string>>(`${this.apiUrl}/ChangePassword`,changePassword)
  }
}
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pagination } from '../../models/Pagination';
import { Location } from '../../models/Location';
import { Response } from '../../models/Response';


@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private apiUrl = 'https://localhost:7072'

  constructor(private http: HttpClient) { }

  getData(page: number, limit: number): Observable<Response<Pagination<Location[]>>> {
    return this.http.get<Response<Pagination<Location[]>>>(`${this.apiUrl}/api/Location/?page=${page}&limit=${limit}`)
  }
}

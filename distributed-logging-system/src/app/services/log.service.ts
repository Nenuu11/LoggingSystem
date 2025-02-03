import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { Observable, switchMap } from 'rxjs';
import { Logs, PaginationParams, ApiResponse } from '../../types';
import { HttpHeaders,HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
@Injectable({
  providedIn: 'root',
})
export class LogService {
  constructor(private apiService: ApiService, private http: HttpClient, private authService : AuthService) {
  }


  
  // getLogs = (
  //   url: string,
  //   params: PaginationParams
  // ): Observable<ApiResponse> => {

  //   return this.apiService.get(url, {
  //     params,
  //     responseType: 'json',
  //   });
  // };

  getLogs(url: string, params: PaginationParams): Observable<ApiResponse> {
    return this.apiService.get<ApiResponse>(url, {
          headers :new HttpHeaders({
            'Authorization': `Bearer ${localStorage.getItem('appToken')}`
          }),
          params,
          responseType: 'json',
        });
  }

  getLogById(url: string ): Observable<ApiResponse> {
        return this.apiService.get<ApiResponse>(url, {
          headers :new HttpHeaders({
            'Authorization': `Bearer ${localStorage.getItem('appToken')}`
          }),
          responseType: 'json',
        });
     
  }

}

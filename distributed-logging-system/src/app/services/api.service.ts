import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Log, Options } from '../../types';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  // this service will contain generic crud operations to be used in other services without
  //invoking httpClient each time we make request
  private baseUrl = 'https://localhost:44345/';  // Your API endpoint
  constructor(private httpClient: HttpClient) {}

  get<T>(endpoint: string, options: Options): Observable<T> {
    return this.httpClient.get<T>(this.baseUrl + endpoint, options) as Observable<T>;
  }

  post<T>(endpoint: string, body: Log, options: Options): Observable<T> {
    return this.httpClient.post<T>(this.baseUrl +endpoint, body, options) as Observable<T>;
  }

  delete<T>(endpoint: string, options: Options): Observable<T> {
    return this.httpClient.delete<T>(this.baseUrl +endpoint, options) as Observable<T>;
  }

  put<T>(endpoint: string, body: Log, options: Options): Observable<T> {
    return this.httpClient.put<T>(this.baseUrl +endpoint, body, options) as Observable<T>;
  }
}

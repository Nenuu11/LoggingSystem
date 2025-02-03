import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, firstValueFrom, map, Observable, of } from 'rxjs';
import { ApiResponse } from '../../types';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {
  }
 
  private readonly TOKEN_KEY = 'appToken';
  
  // setToken(token: string) {
  //   this.cookieService.set('authToken', token, { path: '/', secure: true, sameSite: 'Lax' });
  // }

  // getToken(): string {
  //   return this.cookieService.get('authToken');
  // }

  // getToken(): string {
  //   console.log("get token")
  //   let token = localStorage.getItem(this.TOKEN_KEY);
  //   console.log(`token in getToken -> ${token}`)
  //   console.log(localStorage);
  //   if (token === "null") {
  //     console.log('token empty');
  //     this.http.get<ApiResponse>('https://localhost:44345/v1/get-token', {})
  //       .subscribe({
  //         next: (data: ApiResponse) => {
  //           console.log(data);
  //           token = data.data.toString();
  //         },
  //         error: (error) => {
  //           console.log(error);
  //         },
  //       });
  //     localStorage.setItem(this.TOKEN_KEY, token!);
  //   }
  //   else{
  //     console.log("else")
  //   }

  //   return token!;
  // }

  getToken(): Observable<string> {
    console.log("getToken called");

    // Check if the token exists in localStorage
    let token = localStorage.getItem(this.TOKEN_KEY);
    console.log(`Token in localStorage: ${token}`);

    if (token) {
      // If the token exists, return it as an observable
      console.log("Token found in localStorage");
      return of(token);
    } else {
      // If the token doesn't exist, fetch it from the API
      console.log("Fetching token from API");
      return this.http.get<ApiResponse>('https://localhost:44345/v1/get-token').pipe(
        map((response: ApiResponse) => {
          // Extract the token from the response
          const newToken = response.data.toString();
          console.log("Token received from API:", newToken);

          // Store the token in localStorage
          localStorage.setItem(this.TOKEN_KEY, newToken);
          console.log("Token stored in localStorage");

          // Return the new token
          return newToken;
        }),
        catchError((error) => {
          console.error("Error fetching token:", error);
          throw error; // Re-throw the error to handle it in the calling code
        })
      );
    }
  }

  // deleteToken() {
  //   this.cookieService.delete('authToken', '/');
  // }

  // async fetchAndSaveToken() {
  //   if (!this.getToken()) {
  //     console.log("token is empty");
  //     try {
  //       const response = await firstValueFrom(this.http.get<ApiResponse>('https://localhost:44345/v1/get-token', {}));
  //       this.setToken(response.data.toString());
  //       console.log('Token saved:', response.data.toString());
  //     } catch (error) {
  //       console.error('Token fetch failed:', error);
  //     }
  //   }
  // }
}

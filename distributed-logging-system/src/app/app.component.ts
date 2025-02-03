import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { AuthService } from './services/auth.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  token: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {

    this.authService.getToken().subscribe({
      next: (token) => {
        this.token = token;
        console.log("Token received:", token);
      },
      error: (error) => {
        console.error("Error getting token:", error);
      }
    });
  }
}

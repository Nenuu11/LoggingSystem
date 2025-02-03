// import { bootstrapApplication } from '@angular/platform-browser';
// import { AppComponent } from './app/app.component';
// import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors } from '@angular/common/http';
// import { provideRouter } from '@angular/router';
// import { TokenInterceptor } from './TokenInterceptor';

// bootstrapApplication(AppComponent, {
//   providers: [
//     provideHttpClient(withInterceptors([])),
//     provideRouter([]),
//     { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
//   ]
// }).catch(err => console.error(err));
import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
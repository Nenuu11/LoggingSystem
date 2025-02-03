import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LogComponent } from './components/log/log.component';

export const routes: Routes = [
    {
        path:'',
        component: HomeComponent
    },
    {
        path:'logs',
        component: HomeComponent
    },
    { path: 'logs/:id', component: LogComponent },
];
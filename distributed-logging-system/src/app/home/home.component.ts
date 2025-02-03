import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Log, Logs,ApiResponse } from '../../types';
import { LogService } from '../services/log.service';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  imports: [ButtonModule, TableModule, CardModule, RouterModule]
})
export class HomeComponent {
  constructor(private logService: LogService) {
    console.log('home')
  }

  logs: Log[] = [];
  totalRecords: number = 0;
  rows: number = 5;

  fetchLogs(page: number, perPage: number) {
    console.log('fetch logs')
    this.logService
      .getLogs('v1/logs', { page, perPage })
      .subscribe({
        next: (data: ApiResponse) => {
          console.log(data);
          if(data.statusCode == 200){
             this.logs = data.data as Log[];
             this.totalRecords = (data.data as Logs).total;
          }
          else{
            console.log(data.data);
          }
         
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  ngOnInit() {
    console.log('home init')
    this.fetchLogs(0, this.rows);
  }
}


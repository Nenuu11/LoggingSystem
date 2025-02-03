import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MenubarModule } from 'primeng/menubar';
@Component({
  selector: 'app-header',
  imports: [MenubarModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}

import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.css'],
})
export class NavbarComponent {
  userName: string = '';
  role: string = '';

  constructor(private router: Router) {
    this.userName = localStorage.getItem('name') || '';
    this.role = localStorage.getItem('role') || '';
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}

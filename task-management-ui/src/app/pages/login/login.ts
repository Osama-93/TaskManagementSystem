import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  name = '';
  errorMessage = '';
  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}
  login(): void {
    debugger;
    this.errorMessage = '';

    if (!this.name || this.name.trim() === '') {
      this.errorMessage = 'Username is required';
      return;
    }

    if (this.name.length < 3) {
      this.errorMessage = 'Username must be at least 3 characters long';
      return;
    }

    this.authService.login(this.name).subscribe({
      next: (response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('name', response.username);
        localStorage.setItem('role', response.role);
        this.router.navigate(['/tasks']);
      },
      error: (error) => {
        alert('Login failed: ' + error.error.message);
      },
    });
  }
}

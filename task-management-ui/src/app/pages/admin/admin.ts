import { Component } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../components/navbar/navbar';

@Component({
  selector: 'app-admin',
  imports: [CommonModule, FormsModule, NavbarComponent],
  standalone: true,
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin {
  users: any[] = [];
  username: string = '';
  password: string = '';
  role: string = 'User';

  editingUserId: string | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.http.get(`${environment.apiUrl}/users`).subscribe({
      next: (response: any) => {
        this.users = response;
        console.log('Loading users...');
        console.log(response);
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  createUser(): void {
    const user = {
      username: this.username,
      password: this.password,
      role: this.role,
    };

    this.http.post(`${environment.apiUrl}/users`, user).subscribe({
      next: () => {
        alert('User created successfully');
        this.username = '';
        this.password = '';
        this.role = 'User';
        this.loadUsers();
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  editUser(user: any): void {
    this.editingUserId = user.id;
    this.username = user.username;
    this.password = user.password;
    this.role = user.role;
  }

  saveUser(): void {
    const updatedUser = {
      username: this.username,
      password: this.password,
      role: this.role,
    };

    this.http.put(`${environment.apiUrl}/users/${this.editingUserId}`, updatedUser).subscribe({
      next: () => {
        alert('User updated successfully');
        this.editingUserId = null;
        this.username = '';
        this.password = '';
        this.role = 'User';
        this.loadUsers();
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }
}

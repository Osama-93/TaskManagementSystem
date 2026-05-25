import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { NavbarComponent } from '../../components/navbar/navbar';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './tasks.html',
  styleUrls: ['./tasks.css'],
})
export class TasksComponent implements OnInit {
  tasks: any[] = [];
  title: string = '';
  description: string = '';
  status: number = 1;
  users: any[] = [];
  selectedUserId: string = '';
  isAdmin: boolean = false;
  editingTaskId: string | null = null;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.isAdmin = localStorage.getItem('role') === 'Admin';
    this.loadTasks();
  }

  loadTasks(): void {
    this.loadUsers();
    console.log('Loading tasks for role:', localStorage.getItem('role'));
    if (this.isAdmin) {
      this.taskService.getAllTasks().subscribe({
        next: (response: any) => {
          this.tasks = response;
          console.log(response);
        },
        error: (error: any) => {
          console.error(error);
        },
      });
    } else {
      this.taskService.getMyTasks().subscribe({
        next: (response: any) => {
          this.tasks = response;
          console.log(response);
        },
        error: (error: any) => {
          console.error(error);
        },
      });
    }
  }

  createTask() {
    const task = {
      title: this.title,
      description: this.description,
      status: this.status,
      userId: this.selectedUserId,
    };
    this.taskService.createTask(task).subscribe((response: any) => {
      this.tasks.push(response);
      this.title = '';
      this.description = '';
      this.loadTasks();
    });
  }

  editTask(task: any) {
    this.editingTaskId = task.id;
    this.title = task.title;
    this.description = task.description;
    this.status = task.status;
    this.selectedUserId = task.userId;
  }

  saveTask(): void {
    const updateTask = {
      title: this.title,
      description: this.description,
      status: this.status,
      userId: '',
    };

    this.taskService.updateTask(this.editingTaskId!, updateTask).subscribe({
      next: () => {
        this.editingTaskId = null;
        this.title = '';
        this.description = '';
        this.loadTasks();
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  loadUsers(): void {
    this.taskService.getUsers().subscribe({
      next: (response: any) => {
        debugger;
        if (this.isAdmin) {
          this.users = response;
        } else {
          const userId = localStorage.getItem('userId');
          const user = response.find((u: any) => u.id === userId);
          if (user) {
            this.users = [user];
          }
        }
        var user = response.find((u: any) => u.id === localStorage.getItem('userId'));
        this.users = response;
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getUserName(userId: string): string {
    const user = this.users.find((u: any) => u.id === userId);
    return user ? user.name : 'Unknown';
  }

  updateStatus(task: any, status: number): void {
    this.taskService.updateTaskStatus(task.id, status + 1).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  deleteTask(id: string): void {
    if (!confirm('Are you sure you want to delete this task?')) {
      return;
    }
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.loadTasks();
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  logout() {
    localStorage.removeItem('token');
    window.location.href = '/';
  }
}

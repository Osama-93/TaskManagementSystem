import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get(`${environment.apiUrl}/Users`);
  }

  getUser(id: string) {
    return this.http.get(`${environment.apiUrl}/Users/${id}`);
  }

  getAllTasks() {
    return this.http.get(`${environment.apiUrl}/tasks`);
  }

  getMyTasks() {
    return this.http.get(`${environment.apiUrl}/tasks/my-tasks`);
  }

  createTask(task: any) {
    return this.http.post(`${environment.apiUrl}/tasks`, task);
  }
  updateTask(id: string, task: any) {
    return this.http.put(`${environment.apiUrl}/tasks/${id}`, task);
  }
  updateTaskStatus(id: string, status: number) {
    return this.http.patch(`${environment.apiUrl}/tasks/${id}/status`, { status });
  }
  deleteTask(id: string) {
    return this.http.delete(`${environment.apiUrl}/tasks/${id}`);
  }
}

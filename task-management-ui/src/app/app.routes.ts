import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { TasksComponent } from './pages/tasks/tasks';
import { authGuard } from './guards/auth-guard';
import { adminGuard } from './guards/admin-guard';
import { Admin } from './pages/admin/admin';

export const routes: Routes = [
  {
    path: '',
    component: Login,
  },
  {
    path: 'tasks',
    component: TasksComponent,
    canActivate: [authGuard],
  },
  {
    path: 'admin',
    component: Admin,
    canActivate: [adminGuard],
  },
];

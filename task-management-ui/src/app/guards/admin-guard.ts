import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = () => {
  const router = inject(Router);

  const token = localStorage.getItem('token');
  if (!token) {
    router.navigate(['/']);
    return false;
  }

  const payload = JSON.parse(atob(token.split('.')[1]));

  const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

  if (role !== 'Admin') {
    router.navigate(['/tasks']);
    return false;
  }
  return true;
};

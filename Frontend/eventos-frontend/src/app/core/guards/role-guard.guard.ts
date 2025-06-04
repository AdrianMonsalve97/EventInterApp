import { Injectable, inject } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import {AuthService} from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  private router = inject(Router);
  private authService = inject(AuthService);

  canActivate(): boolean {
    const rol = localStorage.getItem('rol');
    const usuarioNombre = localStorage.getItem('nombre') || 'Usuario Anónimo';

    if (rol === 'Administrador' || usuarioNombre === 'Usuario Anónimo') {
      return true;
    }

    this.router.navigate(['/login']);
    this.authService.logout();
    return false;
  }
}

import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const AuthGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  if (!auth.estaAutenticado()) {
    router.navigate(['/login']);
    return false;
  }

  if (auth.debeCambiarPassword) {
    router.navigate(['/cambiar-password']);
    return false;
  }

  const rolesPermitidos: string[] = route.data['roles'];
  const rolUsuario = auth.rol();

  if (rolesPermitidos && !rolesPermitidos.includes(<string>rolUsuario)) {
    router.navigate(['/login']);
    console.log(`Acceso denegado. Rol requerido: ${rolesPermitidos.join(', ')}, Rol del usuario: ${rolUsuario}`);
    auth.logout();
    return false;
  }

  return true;
};

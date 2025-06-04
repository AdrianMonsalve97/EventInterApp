// usuario.routes.ts
import { Routes } from '@angular/router';

import { AuthGuard } from '../../core/guards/auth-guard.guard';
import {ListarUsuarioComponent} from './listar-usuario/listar-usuario.component';
import {CrearUsuarioComponent} from './crear-usuario/crear-usuario.component';
import {AsistentesUsuarioComponent} from './asistentes-usuario/asistentes-usuario.component';
import {RoleGuard} from '../../core/guards/role-guard.guard';

export const USUARIO_ROUTES: Routes = [
  {
    path: 'usuarios/crear',
    component: CrearUsuarioComponent,
    canActivate: [RoleGuard]
  },
  {
    path: 'usuarios',
    component: ListarUsuarioComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Administrador'] }
  },
  {
    path: 'asistentes',
    loadComponent: () => import('./asistentes-usuario/asistentes-usuario.component').then(m => m.AsistentesUsuarioComponent),
    canActivate: [AuthGuard],
    data: { roles: ['Administrador', 'Expositor', 'Gestionador'] }
  }
];

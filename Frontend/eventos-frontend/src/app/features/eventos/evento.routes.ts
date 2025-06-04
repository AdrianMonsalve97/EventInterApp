import { Routes } from '@angular/router';
import { EventosDisponiblesComponent } from './eventos-disponibles/eventos-disponibles.component';
import { CrearEventosComponent } from './crear-eventos/crear-eventos.component';
import { AuthGuard } from '../../core/guards/auth-guard.guard';

export const eventoRoutes: Routes = [
  {
    path: 'eventos-disponible',
    component: EventosDisponiblesComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Administrador', 'Expositor', 'Gestionador', 'Asistente'] }
  },
  {
    path: 'crear-evento',
    component: CrearEventosComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Expositor', 'Gestionador','Administrador'] }
  },
  {
    path: 'mis-eventos',
    loadComponent: () =>
      import('./mis-eventos/mis-eventos.component').then(m => m.MisEventosComponent),
    canActivate: [AuthGuard],
    data: { roles: ['Administrador', 'Expositor', 'Gestionador', 'Asistente'] }  },
  {
    path: 'editar-evento',
    loadComponent: () =>
      import('./editar-evento/editar-evento.component').then(m => m.EditarEventoComponent),
    canActivate: [AuthGuard],
    data: { roles: ['Expositor', 'Gestionador','Administrador'] }
  }
];

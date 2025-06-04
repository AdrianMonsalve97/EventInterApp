import { Route } from '@angular/router';
import { ListadoInscritosComponent } from './listado-inscritos/listado-inscritos.component';
import { AuthGuard } from '../../core/guards/auth-guard.guard';

export const inscripcionRoutes: Route[] = [
  {
    path: 'inscripciones',
    component: ListadoInscritosComponent,
    canActivate: [AuthGuard],
    data: { roles: ['Administrador', 'Expositor', 'Gestionador', 'Asistente'] }  }
];

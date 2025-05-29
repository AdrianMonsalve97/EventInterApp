import { Routes } from '@angular/router';
import { EventosDisponiblesComponent } from './eventos-disponibles/eventos-disponibles.component';
import {CrearEventosComponent} from './crear-eventos/crear-eventos.component';

export const eventoRoutes: Routes = [
  { path: 'eventos-disponible', component: EventosDisponiblesComponent },
  { path: 'crear-evento', component: CrearEventosComponent },
  {path : 'mis-eventos', loadComponent: () => import('./mis-eventos/mis-eventos.component').then(m => m.MisEventosComponent)},
  {path : 'editar-evento', loadComponent: () => import('./editar-evento/editar-evento.component').then(m => m.EditarEventoComponent)}
];

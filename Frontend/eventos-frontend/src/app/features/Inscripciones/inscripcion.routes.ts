import { Route } from '@angular/router';
import { ListadoInscritosComponent } from './listado-inscritos/listado-inscritos.component';

export const inscripcionRoutes: Route[] = [
  { path: 'inscripciones', component: ListadoInscritosComponent },
];

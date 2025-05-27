import { Routes } from '@angular/router';
import {CrearEventosComponent} from './features/eventos/crear-eventos/crear-eventos.component';
import {AuthGuard} from './core/guards/auth-guard.guard';
import {LoginComponent} from './features/auth/login.component';
import {MainLayoutComponent} from './layout/main-layout/main-layout.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'crear-evento', component: CrearEventosComponent },
      //{ path: 'mis-eventos', component: MisEventosComponent },
      { path: '', redirectTo: 'crear-evento', pathMatch: 'full' },
    ]
  }
];


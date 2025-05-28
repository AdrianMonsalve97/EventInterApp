import { Routes } from '@angular/router';
import { CrearEventosComponent } from './features/eventos/crear-eventos/crear-eventos.component';
import { AuthGuard } from './core/guards/auth-guard.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import {USUARIO_ROUTES} from './features/usuarios/usuario.routes.ts';


export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'crear-evento', component: CrearEventosComponent },
      ...USUARIO_ROUTES,
      { path: '', redirectTo: 'crear-evento', pathMatch: 'full' },
    ]
  }
];

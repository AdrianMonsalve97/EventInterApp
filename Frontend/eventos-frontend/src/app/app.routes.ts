import { CambiarPasswordComponent } from './features/auth/cambiar-password/cambiar-password.component';
import { eventoRoutes } from './features/eventos/evento.routes';
import { USUARIO_ROUTES } from './features/usuarios/usuario.routes.ts';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import {LoginComponent} from './features/auth/login/login.component';
import {AuthGuard} from './core/guards/auth-guard.guard';
import {Routes} from '@angular/router';
import {inscripcionRoutes} from './features/Inscripciones/inscripcion.routes';
import {NotfoundComponent} from './shared/components/notfound/notfound.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'cambiar-password', component: CambiarPasswordComponent },

  {
    path: '',
    component: MainLayoutComponent,
    children: [
      ...USUARIO_ROUTES,
      ...eventoRoutes,
      ...inscripcionRoutes,
      { path: 'eventos-disponible', redirectTo: 'eventos-disponible-disponibles', pathMatch: 'full' },
      {path : '**', component : NotfoundComponent },
    ]

  }
];

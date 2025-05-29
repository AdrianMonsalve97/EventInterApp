import { Routes } from '@angular/router';
import {CrearUsuarioComponent} from './crear-usuario/crear-usuario.component';
import {ListarUsuarioComponent} from './listar-usuario/listar-usuario.component';
import {AsistentesUsuarioComponent} from './asistentes-usuario/asistentes-usuario.component';


export const USUARIO_ROUTES: Routes = [
  { path: 'usuarios/crear', component: CrearUsuarioComponent },
  { path: 'usuarios', component: ListarUsuarioComponent },
  { path: 'asistentes', component: AsistentesUsuarioComponent}
];

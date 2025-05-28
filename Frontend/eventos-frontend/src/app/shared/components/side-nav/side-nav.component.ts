import {Component, inject, signal} from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarModule } from 'primeng/avatar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';


@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [CommonModule, AvatarModule, PanelMenuModule, RouterModule],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {
  usuarioNombreCompleto = signal(localStorage.getItem('nombre') || 'Usuario Anónimo');
  private authService = inject(AuthService);
  private router = inject(Router);
  avatarIniciales = signal('');

  readonly items: MenuItem[] = [
    {
      label: 'Eventos',
      icon: 'pi pi-calendar',
      items: [
        { label: 'Crear Evento', icon: 'pi pi-plus-circle', routerLink: '/crear-evento' },
        { label: 'Eventos Disponibles', icon: 'pi pi-list', routerLink: '/eventos' },
        { label: 'Mis Eventos', icon: 'pi pi-user', routerLink: '/mis-eventos' },
        { label: 'Editar Evento', icon: 'pi pi-pencil', routerLink: '/editar-evento' }
      ]
    },
    {
      label: 'Inscripciones',
      icon: 'pi pi-check-square',
      items: [
        { label: 'Usuarios Inscritos', icon: 'pi pi-users', routerLink: '/inscripciones/evento/1' },
        { label: 'Cancelar Inscripción', icon: 'pi pi-times', routerLink: '/inscripciones/cancelar' }
      ]
    },
    {
      label: 'Usuarios',
      icon: 'pi pi-users',
      items: [
        { label: 'Crear Usuario', icon: 'pi pi-user-plus', routerLink: '/usuarios/crear' },
        { label: 'Listar Usuarios', icon: 'pi pi-list', routerLink: '/usuarios' },
        { label: 'Asistentes', icon: 'pi pi-user', routerLink: '/usuarios/asistentes' }
      ]
    },
    {
      label: 'Cerrar Sesión',
      icon: 'pi pi-sign-out',
      command: () => this.cerrarSesion()
    }
  ];

  constructor() {
    const nombre = this.usuarioNombreCompleto().trim().split(' ');
    const iniciales = nombre.length >= 2
      ? `${nombre[0][0]}${nombre[1][0]}`
      : nombre[0][0] + (nombre[0][1] ?? '');
    this.avatarIniciales.set(iniciales.toUpperCase());

  }
  cerrarSesion() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}

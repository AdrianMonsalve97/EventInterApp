import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AvatarModule } from 'primeng/avatar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import {ButtonDirective} from 'primeng/button';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [CommonModule, AvatarModule, PanelMenuModule, RouterModule, ButtonDirective],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {
  usuarioNombreCompleto = signal(localStorage.getItem('nombre') || 'Usuario Anónimo');
  avatarIniciales = signal('');
  private authService = inject(AuthService);
  protected router = inject(Router);
  items: MenuItem[] = [];
  rutaActual = signal('');
  esAnonimo = signal(false);

  constructor() {
    const nombre = this.usuarioNombreCompleto().trim().split(' ');
    const iniciales = nombre.length >= 2
      ? `${nombre[0][0]}${nombre[1][0]}`
      : nombre[0][0] + (nombre[0][1] ?? '');
    this.avatarIniciales.set(iniciales.toUpperCase());

    this.esAnonimo.set(this.usuarioNombreCompleto() === 'Usuario Anónimo');

    this.router.events.subscribe(() => {
      this.rutaActual.set(this.router.url);
    });

    this.cargarMenuPorRol();
  }

  mostrarBotonVolver() {
    return this.esAnonimo() && this.rutaActual().includes('/usuarios/crear');
  }

  mostrarBotonCerrarSesion() {
    return !this.mostrarBotonVolver();
  }


  cerrarSesion() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  cargarMenuPorRol() {
    const rol = localStorage.getItem('rol');

    const menuBase: MenuItem[] = [];

    if (rol === 'Administrador') {
      menuBase.push(
        {
          label: 'Eventos',
          icon: 'pi pi-calendar',
          items: [
            { label: 'Crear Evento', icon: 'pi pi-plus-circle', routerLink: '/crear-evento' },
            { label: 'Eventos Disponibles', icon: 'pi pi-list', routerLink: '/eventos-disponible' },
            { label: 'Mis Eventos', icon: 'pi pi-user', routerLink: '/mis-eventos' },
            { label: 'Editar Evento', icon: 'pi pi-pencil', routerLink: '/editar-evento' }
          ]
        },
        {
          label: 'Inscripciones',
          icon: 'pi pi-check-square',
          items: [
            { label: 'Usuarios Inscritos', icon: 'pi pi-users', routerLink: '/inscripciones' }
          ]
        },
        {
          label: 'Usuarios',
          icon: 'pi pi-users',
          items: [
            { label: 'Crear Usuario', icon: 'pi pi-user-plus', routerLink: '/usuarios/crear' },
            { label: 'Listar Usuarios', icon: 'pi pi-list', routerLink: '/usuarios' },
            { label: 'Asistentes', icon: 'pi pi-user', routerLink: '/asistentes' }
          ]
        }
      );
    }

    if (rol === 'Gestionador' || rol === 'Expositor') {
      menuBase.push(
        {
          label: 'Eventos',
          icon: 'pi pi-calendar',
          items: [
            { label: 'Crear Evento', icon: 'pi pi-plus-circle', routerLink: '/crear-evento' },
            { label: 'Mis Eventos', icon: 'pi pi-user', routerLink: '/mis-eventos' },
            { label: 'Editar Evento', icon: 'pi pi-pencil', routerLink: '/editar-evento' }
          ]
        },
        {
          label: 'Inscripciones',
          icon: 'pi pi-check-square',
          items: [
            { label: 'Usuarios Inscritos', icon: 'pi pi-users', routerLink: '/inscripciones' }
          ]
        },
        {
          label: 'Usuarios',
          icon: 'pi pi-users',
          items: [
            { label: 'Asistentes', icon: 'pi pi-user', routerLink: '/asistentes' }
          ]
        }
      );
    }

    if (rol === 'Asistente') {
      menuBase.push(
        {
          label: 'Eventos',
          icon: 'pi pi-calendar',
          items: [
            { label: 'Eventos Disponibles', icon: 'pi pi-list', routerLink: '/eventos-disponible' },
            { label: 'Mis Eventos', icon: 'pi pi-user', routerLink: '/mis-eventos' }
          ]
        }
      );
    }

    this.items = menuBase;
  }
}

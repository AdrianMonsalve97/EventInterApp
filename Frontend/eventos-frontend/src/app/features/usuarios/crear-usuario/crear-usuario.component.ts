import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../../core/services/auth.service';
import { UsuarioService } from '../../../core/services/usuario.service';
import {FloatLabel} from 'primeng/floatlabel';
import {Select} from 'primeng/select';
import {CrearUsuarioBody} from '../../../core/models/usuario.model';

@Component({
  selector: 'app-crear-usuario',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    ToastModule,
    FloatLabel,
    Select
  ],
  providers: [MessageService],
  templateUrl: './crear-usuario.component.html',
  styleUrl: './crear-usuario.component.css'
})
export class CrearUsuarioComponent {
  private auth = inject(AuthService);
  private usuarioService = inject(UsuarioService);
  private toast = inject(MessageService);

  id = signal<number | null>(null); // Documento
  nombreUsuario = signal('');
  nombre = signal('');
  email = signal('');
  rol = signal('Asistente'); // Por defecto, Asistente
  roles = [
    { label: 'Administrador', value: 'Administrador' },
    { label: 'Expositor', value: 'Expositor' },
    { label: 'Gestor', value: 'Gestor' }
  ];


  esAdmin = computed(() => localStorage.getItem('rol') === 'Administrador');

  crearUsuario() {
    const creadorId = this.auth.getUsuarioId();

    const payload: CrearUsuarioBody = {
      data: {
        id: this.id() ?? 0,
        nombreUsuario: this.nombreUsuario(),
        nombre: this.nombre(),
        email: this.email(),
        rol: this.esAdmin() ? this.rol() : 'Asistente',
        usuario: creadorId.toString()
      },
      usuario: creadorId.toString()
    };


    this.auth.crearUsuario(payload).subscribe({
      next: (resp) => {
        this.toast.add({ severity: 'success', summary: 'Usuario creado', detail: resp.data });
        this.limpiarFormulario();
      },
      error: (err) => {
        this.toast.add({ severity: 'error', summary: 'Error', detail: err.error?.mensaje ?? 'Falló la creación' });
      }
    });
  }

  limpiarFormulario() {
    this.id.set(null);
    this.nombreUsuario.set('');
    this.nombre.set('');
    this.email.set('');
    this.rol.set('Asistente');
  }
}

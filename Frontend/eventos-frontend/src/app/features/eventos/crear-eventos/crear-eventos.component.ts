import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import {EventService} from '../../../core/services/event.service';
import { ToastModule } from 'primeng/toast';
import {AuthService} from '../../../core/services/auth.service';
import {CrearEventoBody} from '../../../core/models/evento.model';


@Component({
  selector: 'app-crear-eventos',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    InputTextModule,
    TextareaModule,
    CalendarModule,
    InputNumberModule,
    ButtonModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './crear-eventos.component.html',
  styleUrl: './crear-eventos.component.css'
})
export class CrearEventosComponent {
  private eventService = inject(EventService);
  private toast = inject(MessageService);
  private authService = inject(AuthService);
  nombre = signal('');
  descripcion = signal('');
  fechaHora = signal<Date | null>(null);
  ubicacion = signal('');
  capacidadMaxima = signal<number>(1);
   usuarioId = this.authService.getUsuarioId();

  formularioValido = computed(() =>
    this.nombre().trim() &&
    this.descripcion().trim() &&
    this.fechaHora() &&
    this.ubicacion().trim() &&
    this.capacidadMaxima() > 0
  );

  crearEvento() {
    if (!this.formularioValido()) {
      this.toast.add({
        severity: 'warn',
        summary: 'Formulario inválido',
        detail: 'Por favor completa todos los campos.',
        life: 3000
      });
      return;
    }

    const request: CrearEventoBody = {
      data: {
        nombre: this.nombre(),
        descripcion: this.descripcion(),
        fechaHora: this.fechaHora()!,
        ubicacion: this.ubicacion(),
        capacidadMaxima: this.capacidadMaxima(),
        usuario: this.usuarioId
      },
      usuario: this.usuarioId
    };


    this.eventService.crearEvento(request).subscribe({
      next: (resp) => {
        console.log('RESPUESTA DEL BACKEND:', resp); // 👈 ¿Esto aparece?
        this.toast.add({ severity: 'success', summary: 'OK', detail: resp?.data });
      },
      error: (err) => {
        console.error('ERROR DEL BACKEND:', err); // 👈 ¿Esto aparece?
        this.toast.add({ severity: 'error', summary: 'Error', detail: err.error?.mensaje ?? 'Error interno' });
      }
    });


  }

  private limpiarFormulario() {
    this.nombre.set('');
    this.descripcion.set('');
    this.fechaHora.set(null);
    this.ubicacion.set('');
    this.capacidadMaxima.set(1);
  }
}

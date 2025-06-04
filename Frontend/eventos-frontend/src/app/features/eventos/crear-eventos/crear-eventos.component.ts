import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { EventService } from '../../../core/services/event.service';
import { ToastModule } from 'primeng/toast';
import { AuthService } from '../../../core/services/auth.service';
import { CrearEventoBody } from '../../../core/models/evento.model';
import { FloatLabel } from 'primeng/floatlabel';
import { DatePicker } from 'primeng/datepicker';
import { Router } from '@angular/router';

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
    ToastModule,
    FloatLabel,
    DatePicker
  ],
  providers: [MessageService],
  templateUrl: './crear-eventos.component.html',
  styleUrl: './crear-eventos.component.css'
})
export class CrearEventosComponent {
  private eventService = inject(EventService);
  private message = inject(MessageService);
  private authService = inject(AuthService);
  private router = inject(Router); // 👉 Inyectamos el Router

  nombre = signal('');
  descripcion = signal('');
  fechaHora = signal<Date | null>(null);
  ubicacion = signal('');
  capacidadMaxima = signal<number>(1);
  usuarioId = this.authService.getUsuarioId();

  formularioValido = computed(() => {
    const nombreValido = this.nombre().trim().length > 0;
    const descripcionValida = this.descripcion().trim().length > 0;
    const fechaHoraValida = this.fechaHora() !== null;
    const ubicacionValida = this.ubicacion().trim().length > 0;
    const capacidadMaximaValida = this.capacidadMaxima() > 0;
    return (
      nombreValido &&
      descripcionValida &&
      fechaHoraValida &&
      ubicacionValida &&
      capacidadMaximaValida
    );
  });

  crearEvento() {
    if (!this.formularioValido()) {
      this.message.add({
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
      usuario: this.usuarioId.toString()
    };

    this.eventService.crearEvento(request).subscribe({
      next: (resp) => {
        this.message.add({
          severity: 'success',
          summary: 'OK',
          detail: resp?.data,
          life: 3000,
          styleClass: 'custom-success-toast'
        });

        // 🔁 Redirección con pequeño delay para dejar ver el toast
        setTimeout(() => {
          this.router.navigate(['/eventos-disponible']);
        }, 500);
      },
      error: (err) => {
        this.message.add({
          severity: 'error',
          summary: 'Error',
          detail: err.error?.mensaje ?? 'Error interno',
          life: 3000,
          styleClass: 'custom-success-toast'
        });
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

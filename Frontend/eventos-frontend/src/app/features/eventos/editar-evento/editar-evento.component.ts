import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { TableModule } from 'primeng/table';
import { EventService } from '../../../core/services/event.service';
import {
  EventoResumen,
  EventoDetalle,
  EditarEventoRequest,
  EliminarEventoRequest
} from '../../../core/models/evento.model';

@Component({
  selector: 'app-editar-evento',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    CalendarModule,
    InputNumberModule,
    DropdownModule,
    ButtonModule,
    ToastModule,
    TableModule
  ],
  providers: [MessageService],
  templateUrl: './editar-evento.component.html'
})
export class EditarEventoComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private eventoService = inject(EventService);
  private toast = inject(MessageService);

  cargando = signal(false);
  eventoCargado = signal(false);
  eventoSeleccionadoManual = signal(false);
  asistentes = signal<any[]>([]);
  eventosDisponibles = signal<EventoResumen[]>([]);
  eventoIdSeleccionado: number | null = null;
  form = this.fb.group({
    nombre: this.fb.control<string | null>('', Validators.required),
    descripcion: this.fb.control<string | null>(''),
    fechaHora: this.fb.control<Date | null>(null, Validators.required),
    ubicacion: this.fb.control<string | null>('', Validators.required),
    capacidadMaxima: this.fb.control<number | null>(1, [Validators.required, Validators.min(1)])
  });

  constructor() {
    this.form.disable();
    this.cargarEventosDisponibles();
  }

  cargarEventosDisponibles() {
    this.eventoService.listarEventosResumen().subscribe({
      next: eventos => this.eventosDisponibles.set(eventos),
      error: () => {
        this.toast.add({ severity: 'error', summary: 'Error', detail: 'No se pudieron cargar los eventos' });
      }
    });
  }

  onSeleccionarEvento(id: number) {
    if (!id) return;
    this.eventoSeleccionadoManual.set(true);
    this.obtenerEvento(id);
  }

  obtenerEvento(id: number) {
    this.cargando.set(true);
    this.form.disable();

    this.eventoService.obtenerEventoPorId(id).subscribe({
      next: (evento: EventoDetalle) => {
        const { nombre, descripcion, fechaHora, ubicacion, capacidadMaxima } = evento;
        this.form.patchValue({ nombre, descripcion, fechaHora, ubicacion, capacidadMaxima });
        this.asistentes.set(evento.asistentes ?? []);
        this.eventoCargado.set(true);
        this.form.enable();
        this.cargando.set(false);
      },
      error: () => {
        this.toast.add({ severity: 'error', summary: 'Error', detail: 'No se pudo cargar el evento' });
        this.form.disable();
        this.eventoCargado.set(false);
        this.cargando.set(false);
      }
    });
  }

  private limpiarNulls<T extends object>(obj: T): T {
    const limpio: any = {};
    for (const [key, value] of Object.entries(obj)) {
      limpio[key] = value === null ? undefined : value;
    }
    return limpio;
  }

  guardarCambios() {
    if (this.form.invalid || !this.eventoSeleccionadoManual() || !this.eventoCargado() || !this.eventoIdSeleccionado) return;

    const valores = this.form.value;
    const idUsuarioLocal = localStorage.getItem('usuarioId');
    if (!idUsuarioLocal) {
      this.toast.add({
        severity: 'error',
        summary: 'Error',
        detail: 'No se pudo obtener el ID del usuario'
      });
      return;
    }

    const payload: EditarEventoRequest = {
      idEvento: this.eventoIdSeleccionado,
      nuevaFechaHora: valores.fechaHora!,
      nuevaUbicacion: valores.ubicacion!,
      nuevaCapacidadMaxima: valores.capacidadMaxima!,
      idUsuario: Number(idUsuarioLocal)
    };

    this.eventoService.editarEvento(payload).subscribe({
      next: () => {
        this.toast.add({
          severity: 'success',
          summary: 'Evento actualizado',
          detail: 'Los cambios fueron guardados correctamente'
        });
        this.router.navigate(['/eventos']);
      },
      error: () => {
        this.toast.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudo editar el evento'
        });
      }
    });
  }
  eliminarEvento() {
    if (!this.eventoIdSeleccionado) return;

    const idUsuarioLocal = localStorage.getItem('usuarioId');
    if (!idUsuarioLocal) {
      this.toast.add({ severity: 'error', summary: 'Error', detail: 'No se pudo obtener el ID del usuario' });
      return;
    }

    const payload: EliminarEventoRequest = {
      idEvento: this.eventoIdSeleccionado,
      idUsuario: Number(idUsuarioLocal)
    };

    this.eventoService.eliminarEvento(payload).subscribe({
      next: (response) => {
        this.toast.add({
          severity: 'success',
          summary: 'Evento eliminado',
          detail: response?.mensaje ?? 'Evento eliminado correctamente'
        });
        this.router.navigate(['/eventos']);
      },
      error: (err) => {
        const mensajeError = err.error?.mensaje ?? 'No se pudo eliminar el evento';
        this.toast.add({
          severity: 'error',
          summary: 'Error',
          detail: mensajeError
        });
      }
    });

  }
}

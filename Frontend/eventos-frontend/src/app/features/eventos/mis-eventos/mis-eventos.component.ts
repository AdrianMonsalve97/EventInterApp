import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { EventService } from '../../../core/services/event.service';
import { InscripcionServiceService } from '../../../core/services/inscripcion.service.service';
import { ButtonModule } from 'primeng/button';
import { EventoResponse } from '../../../core/models/evento.model';

@Component({
  selector: 'app-mis-eventos',
  standalone: true,
  imports: [CommonModule, TableModule, ToastModule, ButtonModule],
  providers: [MessageService],
  templateUrl: './mis-eventos.component.html',
  styleUrls: ['./mis-eventos.component.css']
})
export class MisEventosComponent implements OnInit {
  private eventService = inject(EventService);
  private inscripcionService = inject(InscripcionServiceService);
  private messageService = inject(MessageService);

  eventosInscritos = signal<EventoResponse[]>([]);

  ngOnInit() {
    this.cargarInscripciones();
  }

  cargarInscripciones() {
    this.eventService.listarMisEventos().subscribe({
      next: (resp) => this.eventosInscritos.set(resp),
      error: (err) => console.error('Error al cargar mis eventos', err)
    });
  }

  cancelarInscripcion(idEvento: number) {
    const idUsuario = Number(localStorage.getItem('usuarioId'));

    this.inscripcionService.cancelarInscripcion(idUsuario, idEvento).subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Cancelado',
          detail: 'Inscripción eliminada'
        });
        this.cargarInscripciones();
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'No se pudo cancelar la inscripción'
        });
      }
    });
  }
}

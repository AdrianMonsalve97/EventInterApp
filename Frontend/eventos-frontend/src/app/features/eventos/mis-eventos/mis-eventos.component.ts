import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import {EventService} from '../../../core/services/event.service';
import {EventoResponse} from '../../../core/models/evento.model';


@Component({
  selector: 'app-mis-eventos',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './mis-eventos.component.html',
  styleUrls: ['./mis-eventos.component.css']
})
export class MisEventosComponent {
  private eventService = inject(EventService);
  eventosInscritos = signal<EventoResponse[]>([]);

  constructor() {
    this.eventService.listarMisEventos().subscribe({
      next: (resp) => this.eventosInscritos.set(resp),
      error: (err) => console.error('Error al cargar mis eventos', err)
    });
  }
}

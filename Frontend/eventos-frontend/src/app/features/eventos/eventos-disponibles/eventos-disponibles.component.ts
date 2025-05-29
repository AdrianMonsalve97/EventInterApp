import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { EventService } from '../../../core/services/event.service';

@Component({
  selector: 'app-eventos-disponibles',
  standalone: true,
  imports: [CommonModule, TableModule, ButtonModule, ToastModule],
  providers: [MessageService],
  templateUrl: './eventos-disponibles.component.html',
  styleUrls: ['./eventos-disponibles.component.css']
})
export class EventosDisponiblesComponent {
  private eventService = inject(EventService);
  private toast = inject(MessageService);

  readonly eventos = this.eventService.eventosDisponibles;

  inscribirse(id: number) {
    this.eventService.inscribirse(id).subscribe({
      next: () => {
        this.toast.add({ severity: 'success', summary: 'Inscripción exitosa', detail: '¡Estás inscrito en el evento!' });
        this.eventService.refreshEventosDisponibles(); // recargar
      },
      error: (err) => {
        this.toast.add({ severity: 'error', summary: 'Error', detail: err.error?.mensaje ?? 'No se pudo inscribir' });
      }
    });
  }
}

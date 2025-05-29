import { Component, inject, signal, computed } from '@angular/core';
import { UsuarioService } from '../../../core/services/usuario.service';
import { Usuario } from '../../../core/models/usuario.model';
import { EventService } from '../../../core/services/event.service';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { FormsModule } from '@angular/forms'; // ¡Importante para ngModel!
import { CommonModule } from '@angular/common';
import {EventoResumen} from '../../../core/models/evento.model';


@Component({
  selector: 'app-asistentes-usuario',
  standalone: true,
  imports: [
    CommonModule,
    DropdownModule,
    TableModule,
    FormsModule
  ],
  templateUrl: './asistentes-usuario.component.html',
  styleUrls: ['./asistentes-usuario.component.css']
})

export class AsistentesUsuarioComponent {
  private eventoService = inject(EventService);
  private usuarioService = inject(UsuarioService);

  eventos = signal<EventoResumen[]>([]);
  eventoSeleccionadoId = signal<number | null>(null);
  asistentes = signal<Usuario[]>([]);

  constructor() {
    this.eventoService.listarEventosResumen().subscribe({
      next: (resp) => {
        this.eventos.set(resp);
      },
      error: (err) => {
        console.error('Error cargando eventos', err);
      }
    });
  }
  onEventoSeleccionadoChange(id: number) {
    this.eventoSeleccionadoId.set(id);
    this.cargarAsistentes(id);
  }

  cargarEventos() {
    this.eventoService.listarEventosResumen().subscribe({
      next: (resp) => {
        this.eventos.set(resp);
      },
      error: (err) => {
        console.error('Error cargando eventos', err);
      }
    });
  }

  cargarAsistentes(eventoId: number) {
    this.usuarioService.listarAsistentes(eventoId).subscribe({
      next: (resp) => {
        this.asistentes.set(resp);
      },
      error: (err) => {
        console.error('Error cargando asistentes', err);
      }
    });
  }

}

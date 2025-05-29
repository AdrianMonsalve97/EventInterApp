import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { InscripcionResponse } from '../../../core/models/inscripcion.model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import {InscripcionServiceService} from '../../../core/services/inscripcion.service.service';

@Component({
  selector: 'app-listado-inscritos',
  standalone: true,
  templateUrl: './listado-inscritos.component.html',
  styleUrls: ['./listado-inscritos.component.css'],
  imports: [CommonModule, TableModule, ButtonModule, ToastModule],
  providers: [MessageService]
})
export class ListadoInscritosComponent {
  private route = inject(ActivatedRoute);
  private inscripcionService = inject(InscripcionServiceService);
  private toast = inject(MessageService);

  inscritos = signal<InscripcionResponse[]>([]);
  idEvento = signal<number>(0);

  constructor() {
    const id = this.route.snapshot.queryParamMap.get('idEvento');
    if (id) {
      this.idEvento.set(+id);
      this.cargarInscripciones();
    }
  }

  cargarInscripciones() {
    this.inscripcionService.listarInscripcionesPorUsuario(this.idEvento()).subscribe({
      next: data => this.inscritos.set(data),
      error: () => this.toast.add({ severity: 'error', summary: 'Error', detail: 'No se pudieron cargar los inscritos' })
    });
  }

  cancelarInscripcion(idUsuario: number) {
    this.inscripcionService.cancelarInscripcion(idUsuario, this.idEvento()).subscribe({
      next: () => {
        this.toast.add({ severity: 'success', summary: 'Cancelado', detail: 'Inscripción eliminada' });
        this.cargarInscripciones();
      },
      error: () => {
        this.toast.add({ severity: 'error', summary: 'Error', detail: 'No se pudo cancelar la inscripción' });
      }
    });
  }
}

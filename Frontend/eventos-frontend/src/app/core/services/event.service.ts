import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toSignal, toObservable } from '@angular/core/rxjs-interop';
import {
  CrearEventoBody,
  CrearEventoRequest, EditarEventoRequest,
  EventoDetalle,
  EventoResponse,
  EventoResumen
} from '../models/evento.model';
import { Observable, of } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { environment } from '../../../environment/environment';
import { RespuestaGeneral } from '../models/respuesta-general';
import { UsuarioListadoDto } from '../models/usuario.model';

@Injectable({ providedIn: 'root' })
export class EventService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/eventos`;

  private recargaEventos = signal(0);

  readonly eventosDisponibles = toSignal(
    toObservable(this.recargaEventos).pipe(
      switchMap(() =>
        this.http.get<EventoResponse[]>(`${this.apiUrl}/disponibles`).pipe(
          catchError(() => of([]))
        )
      )
    ),
    { initialValue: [] }
  );

  refreshEventosDisponibles() {
    this.recargaEventos.update(v => v + 1);
  }

  crearEvento(payload: CrearEventoBody): Observable<RespuestaGeneral<string>> {
    return this.http.post<RespuestaGeneral<string>>(`${this.apiUrl}/crear`, payload);
  }

  inscribirse(idEvento: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/inscribirse`, { idEvento });
  }

  listarMisEventos(): Observable<EventoResponse[]> {
    const id = localStorage.getItem('usuarioId');
    if (!id) throw new Error('No hay usuario logueado');

    return this.http.get<EventoResponse[]>(`${this.apiUrl}/mis-eventos?idUsuario=${id}`);
  }


  editarEvento(request: EditarEventoRequest): Observable<any> {
    return this.http.put(`${this.apiUrl}/editar`, request);
  }
  eliminarEvento(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/eliminar?id=${id}`);
  }

  listarEventosResumen(): Observable<EventoResumen[]> {
    return this.http.get<EventoResumen[]>(`${this.apiUrl}/nombres`);
  }

  listarAsistentesPorEvento(idEvento: number): Observable<UsuarioListadoDto[]> {
    return this.http.get<UsuarioListadoDto[]>(`${this.apiUrl}/asistentes?idEvento=${idEvento}`);
  }

  obtenerEventoPorId(id: number): Observable<EventoDetalle> {
    return this.http.get<EventoDetalle>(`${this.apiUrl}/detalle/${id}`);
  }
}

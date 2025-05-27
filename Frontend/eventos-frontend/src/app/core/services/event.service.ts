import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toSignal, toObservable } from '@angular/core/rxjs-interop';
import {CrearEventoBody, CrearEventoRequest, EventoResponse} from '../models/evento.model';
import { Observable, of } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import {environment} from '../../../environment/environment';
import {RespuestaGeneral} from '../models/respuesta-general';

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

  // Eventos en los que el usuario está inscrito
  readonly misEventos = toSignal(
    this.http.get<EventoResponse[]>(`${this.apiUrl}/mis-eventos`).pipe(
      catchError(() => of([]))
    ),
    { initialValue: [] }
  );

  crearEvento(payload: CrearEventoBody): Observable<RespuestaGeneral<string>> {
    return this.http.post<RespuestaGeneral<string>>(`${this.apiUrl}/crear`, payload);
  }


  inscribirse(idEvento: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/inscribirse`, { idEvento });
  }

  editarEvento(id: number, cambios: Partial<CrearEventoRequest>): Observable<any> {
    return this.http.put(`${this.apiUrl}/editar`, { id, ...cambios });
  }

  eliminarEvento(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/eliminar?id=${id}`);
  }
}

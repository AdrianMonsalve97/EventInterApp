import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {environment} from '../../../environment/environment';
import {Observable} from 'rxjs';
import { InscripcionResponse} from '../models/inscripcion.model';
import {InscripcionRequest} from '../models/inscripcion.model';

@Injectable({
  providedIn: 'root'
})
export class InscripcionServiceService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}`;


  inscribirAEvento(payload: InscripcionRequest): Observable<InscripcionResponse[]> {
    const params = new HttpParams().set('idUsuario', 'eventoId');
    return this.http.post<InscripcionResponse[]>(`${this.apiUrl}/eventos/inscribirse`, payload, { params });
  }
  listarInscripcionesPorUsuario(idEvento: number): Observable<InscripcionResponse[]> {
    const params = new HttpParams().set('idEvento', idEvento);
    return this.http.get<InscripcionResponse[]>(`${this.apiUrl}/inscripciones/evento/${idEvento}`, { params });
  }
  cancelarInscripcion(idUsuario: number, idEvento: number): Observable<any> {
    const params = new HttpParams().set('idUsuario', idUsuario).set('idEvento', idEvento);
    return this.http.delete(`${this.apiUrl}/inscripciones/cancelar`, { params });
  }




}

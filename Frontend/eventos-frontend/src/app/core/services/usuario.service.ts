import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environment/environment';
import {Usuario, UsuarioAsistente} from '../models/usuario.model';
import { RespuestaGeneral } from '../models/respuesta-general';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/usuarios`;

  listarUsuarios(): Observable<Usuario[]> {
    const idSolicitante = localStorage.getItem('usuarioId');
    if (!idSolicitante) throw new Error('No hay usuario logueado');

    const params = new HttpParams().set('idSolicitante', idSolicitante);

    return this.http.get<Usuario[]>(this.apiUrl, { params });
  }

  listarAsistentes(eventoId: number): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(`${this.apiUrl}/asistentes?idEvento=${eventoId}`);
  }


}

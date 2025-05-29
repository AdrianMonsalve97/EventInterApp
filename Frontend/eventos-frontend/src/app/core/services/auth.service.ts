import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environment/environment';
import {CrearUsuarioBody} from '../models/usuario.model';
import {CambiarPasswordRequest} from '../models/password.model';
import {Observable} from 'rxjs';

export type LoginRequest = {
  nombreUsuario: string;
  password: string;
};

export type LoginResponse = {
  token: string;
  idUsuario: number;
  nombreUsuario: string;
  nombre: string;
  rol: string;
  debeCambiarPassword: boolean;
};

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly baseUrl = environment.apiUrl + '/Auth';

  isAuthenticated = signal(false);
  rol = signal<string | null>(null);
  nombre = signal<string | null>(null);
  usuarioId = signal<number | null>(null); // ✅ nuevo signal

  constructor(private http: HttpClient) {}

  login(data: LoginRequest) {
    return this.http.post<{ data: LoginResponse }>(`${this.baseUrl}/login`, data);
  }

  guardarSesion(res: LoginResponse) {
    localStorage.setItem('token', res.token);
    localStorage.setItem('usuarioId', res.idUsuario.toString());
    localStorage.setItem('nombre', res.nombre);
    localStorage.setItem('rol', res.rol);
    localStorage.setItem('debeCambiarPassword', String(res.debeCambiarPassword));

    this.usuarioId.set(res.idUsuario); // ✅
    this.isAuthenticated.set(true);
    this.rol.set(res.rol);
    this.nombre.set(res.nombre);
  }

  logout() {
    localStorage.clear();
    this.isAuthenticated.set(false);
    this.rol.set(null);
    this.nombre.set(null);
    this.usuarioId.set(null); // ✅
  }

  cargarSesionDesdeStorage() {
    const id = localStorage.getItem('usuarioId');
    const nombre = localStorage.getItem('nombre');
    const rol = localStorage.getItem('rol');

    if (id && nombre && rol) {
      this.usuarioId.set(+id);
      this.nombre.set(nombre);
      this.rol.set(rol);
      this.isAuthenticated.set(true);
    }
  }

  getUsuarioId(): number {
    return this.usuarioId() ?? +localStorage.getItem('usuarioId')!;
  }
  crearUsuario(payload: CrearUsuarioBody) {
    return this.http.post<{ data: string }>(`${this.baseUrl}/registrar`, payload);
  }
  cambiarPassword(payload: CambiarPasswordRequest): Observable<any> {
    return this.http.put(`${this.baseUrl}/cambiarpassword`, payload);
  }
  get debeCambiarPassword(): boolean {
    return localStorage.getItem('debeCambiarPassword') === 'true';
  }
  estaAutenticado(): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;

    const exp = JSON.parse(atob(token.split('.')[1])).exp;
    return Date.now() / 1000 < exp;
  }



}

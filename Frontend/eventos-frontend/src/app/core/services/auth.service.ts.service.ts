import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/enviroment';

export type LoginRequest = {
  username: string;
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
  private readonly baseUrl = environment.apiUrl + '/auth';

  isAuthenticated = signal(false);
  rol = signal<string | null>(null);
  nombre = signal<string | null>(null);

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

    this.isAuthenticated.set(true);
    this.rol.set(res.rol);
    this.nombre.set(res.nombre);
  }

  logout() {
    localStorage.clear();
    this.isAuthenticated.set(false);
    this.rol.set(null);
    this.nombre.set(null);
  }
}

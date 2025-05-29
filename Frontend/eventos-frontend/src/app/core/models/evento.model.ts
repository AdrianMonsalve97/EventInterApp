export interface CrearEventoRequest {
  nombre: string;
  descripcion: string;
  fechaHora: Date;
  ubicacion: string;
  capacidadMaxima: number;
}

export interface EventoResponse {
  id: number;
  nombre: string;
  descripcion: string;
  fechaHora: Date;
  ubicacion: string;
  capacidadMaxima: number;
  cantidadInscritos: number;
  inscrito: boolean;
}
export interface CrearEventoRequest {
  nombre: string;
  descripcion: string;
  fechaHora: Date;
  ubicacion: string;
  capacidadMaxima: number;
  usuario: number;
}
export interface CrearEventoBody {
  data: CrearEventoRequest & { usuario: number };
  usuario: string;
}
export interface EventoResumen {
  id: number;
  nombre: string;
}
export interface EventoDetalle {
  id: number;
  nombre: string;
  descripcion?: string;
  fechaHora: Date;
  ubicacion: string;
  capacidadMaxima: number;
  asistentes: { nombre: string; email: string }[];
}

export interface EditarEventoRequest {
  idEvento: number;
  nuevaFechaHora: Date;
  nuevaUbicacion: string;
  nuevaCapacidadMaxima: number;
  idUsuario: number;
}

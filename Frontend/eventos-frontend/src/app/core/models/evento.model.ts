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

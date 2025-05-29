export interface Usuario {
  id: number;
  nombre: string;
  nombreUsuario: string;
  email: string;
  rol: string;
  debeCambiarPassword: boolean;
}

export interface CrearUsuarioCommand {
  id: number;
  nombreUsuario: string;
  nombre: string;
  email: string;
  rol: string;
  usuario: string;
}

export interface CrearUsuarioBody {
  data: CrearUsuarioCommand;
  usuario: string;
}

export interface UsuarioAsistente {
  id: number;
  nombre: string;
  nombreUsuario: string;
  email: string;
  rol: 'Asistente';
}
export interface UsuarioListadoDto {
  id: number;
  nombre: string;
  nombreUsuario: string;
  email: string;
  rol: string;
  idEvento: number;
}

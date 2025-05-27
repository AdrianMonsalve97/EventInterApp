export interface RespuestaGeneral<T> {
  data: T;
  error: boolean;
  mensaje?: string;
}

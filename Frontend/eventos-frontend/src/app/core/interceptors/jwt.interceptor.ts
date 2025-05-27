import { inject } from '@angular/core';
import {
  HttpInterceptorFn,
  HttpRequest,
  HttpHandlerFn,
  HttpHeaders
} from '@angular/common/http';
import {environment} from '../../../environment/environment';

export const jwtInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const token = localStorage.getItem('token');

  if (token && !req.url.endsWith('/login')) {
    const updatedHeaders = req.headers
      .set('Authorization', `Bearer ${token}`)
      .set('Cliente', environment.clienteHeader)

    const authReq = req.clone({ headers: updatedHeaders });
    return next(authReq);
  }

  return next(req);
};

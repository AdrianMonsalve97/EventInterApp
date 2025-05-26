import { inject } from '@angular/core';
import {
  HttpInterceptorFn,
  HttpRequest,
  HttpHandlerFn,
  HttpHeaders
} from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const token = localStorage.getItem('token');

  if (token && !req.url.endsWith('/login')) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      Cliente: 'Resiliencia'
    });

    const authReq = req.clone({ headers });
    return next(authReq);
  }

  return next(req);
};

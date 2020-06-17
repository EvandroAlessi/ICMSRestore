import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {
  constructor(
    private authentication: AuthenticationService,
    private toast: ToastrService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status == 500) {
          this.toast.error(
            'Ocorreu um erro. Por favor, tente novamente.',
            'Erro!'
          );
        } else {
          if (err.status == 401 || err.status == 403) {
            if (
              !request.url.endsWith('logout') &&
              !request.url.endsWith('login')
            ) {
              this.toast.error('Não autenticado.');
              this.authentication.logout();
            } else {
              if (request.url.endsWith('login')) {
                this.toast.error(err.error.message, 'Erro!');
              }
            }
          } else {
            if (err.error.message) {
              this.toast.error(err.error.message, 'Erro!');
            }
          }
        }
        return throwError(err);
      })
    );
  }
}

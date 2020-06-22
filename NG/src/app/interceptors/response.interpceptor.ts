import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {
  constructor(
    private authentication: AuthenticationService,
    private toast: ToastrService
  ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      tap(event => {
        if (event && event instanceof HttpResponse) {
          if(event.body){
            if(event.body.pagination) {
              event = event.clone({ body: this.modifyBody(event.body) });
            }
          }
        }   

        return event;
      }),
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
  
  private modifyBody(body: any) {
    const end = body.pagination.currentPage + 4 > body.pagination.pageCount
        ? body.pagination.pageCount
        : body.pagination.currentPage + 4;

    const start = end - 9 < 1 
        ? 1 
        : end - 9;

    body.pagination.pages = [];

    for (let i = start; i <= end; i++) {
      body.pagination.pages.push(i);
    }

    return body;
  }
}

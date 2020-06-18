import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';

function addToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
  return request.clone({
    headers: new HttpHeaders({ "Authorization" : `Bearer ${ token }`})
  });
}

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const user = JSON.parse(localStorage.getItem('user'));

    if (user && user.token) {
      request = addToken(request, user.token);
    } 
    
    return next.handle(request);
  }
}

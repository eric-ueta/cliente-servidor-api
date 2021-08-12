import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAndToken } from '../models/user-and-token';

function addToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
  return request.clone({
    headers: new HttpHeaders({ token: `${token}` }),
  });
}

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const userStored = localStorage.getItem('user');
    let user: UserAndToken;

    if (userStored) {
      user = JSON.parse(userStored);

      if (user.token) {
        request = addToken(request, user.token);
      }
    }

    return next.handle(request);
  }
}

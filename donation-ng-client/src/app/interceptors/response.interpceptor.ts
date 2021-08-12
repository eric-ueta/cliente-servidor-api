import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {
  constructor(
    private authentication: AuthenticationService,
    private toast: ToastrService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      tap((event) => {
        return event;
      }),
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          switch (err.status) {
            case 400:
              if (err?.error?.message) {
                console.log(err);
                alert(err.error.message);
              } else if (err?.error || err?.error?.errors) {
                let validationErrors = '';

                if (err?.error?.errors) {
                  for (const key in err.error.errors) {
                    if (err.error.errors[key][0]) {
                      validationErrors += err.error.errors[key][0] + '\n';
                    }
                  }
                }

                this.toast.error(
                  validationErrors ||
                    err?.error.title ||
                    err?.message ||
                    'Erro desconhecido' 
                );
              }
              break;
            case 401:
              this.toast.error('Usuário não autenticado.');

              // if (this.authentication.isAuthenticated()) {
              //   this.authentication.logout();
              // }
              break;
            case 403:
              this.toast.warning('Você não tem permissão para fazer isso.');
              this.router.navigate(['/']);
              break;
            case 500:
              this.toast.error(
                err?.message ||
                  'Erro desconhecido. Por favor, tente novamente.'
              );

              break;
            default:
              break;
          }
        }

        return throwError(err);
      })
    );
  }
}

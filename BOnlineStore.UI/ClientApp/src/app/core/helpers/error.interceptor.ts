import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../services/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401) {
          // auto logout if 401 response returned from api
          this.router.navigate(['/auth/errors/page-401']);
          /*this.authenticationService.logout();
          location.reload();*/
        }

        debugger;
        var error = err.error.message || err.statusText;

        if (err?.error?.errors) {
          error = '';
          error += err.error.title + '\n';

          Object.entries(err.error.errors).forEach(
            ([key, value]) => (error += `${key}: ${value}` + '\n')
          );

          /*err.error.errors.forEach((element: any) => {
            error += element.Message + '\n';
          });*/
        }

        throw new Error(error);
      })
    );
  }
}

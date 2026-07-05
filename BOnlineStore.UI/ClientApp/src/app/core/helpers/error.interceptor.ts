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
  ) { }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        // Handle 401 Unauthorized errors
        if (err.status === 401) {
          this.router.navigate(['/auth/errors/page-401']);
          return throwError(() => err);
        }

        const errorMessage = this.extractErrorMessage(err);
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  /**
   * Extracts a user-friendly error message from the HTTP error response
   * @param err HTTP error response object
   * @returns Formatted error message string
   */
  private extractErrorMessage(err: any): string {
    // Handle ASP.NET Core validation errors (lowercase 'errors')
    if (err?.error?.errors) {
      return this.formatErrors(err.error.errors, err.error.title);
    }

    // Handle FluentValidation errors (uppercase 'Errors')
    if (err?.error?.Errors) {
      return this.formatErrors(err.error.Errors);
    }

    // Default error message
    return (
      err.error?.message || err.statusText || 'An unexpected error occurred'
    );
  }

  /**
   * Formats validation errors from various sources (ASP.NET Core, FluentValidation, Identity)
   * @param errors Validation errors object
   * @param title Optional error title to prepend
   * @returns Formatted error string
   */
  private formatErrors(errors: any, title?: string): string {
    let errorMessage = title ? `${title}\n` : '';

    Object.entries(errors).forEach(([key, value]: [string, any]) => {
      // Handle object with Message property (FluentValidation or Identity format)
      if (value && typeof value === 'object') {
        if ('Message' in value && value.Message) {
          errorMessage += `${value.Message}\n`;
        } else if ('message' in value && value.message) {
          errorMessage += `${value.message}\n`;
        } else {
          // Fallback for unknown object format
          errorMessage += `${key}: ${JSON.stringify(value)}\n`;
        }
      } else {
        // Handle simple string or array values
        errorMessage += `${key}: ${value}\n`;
      }
    });

    return errorMessage.trim();
  }
}

import { CoreConfig } from '@core/types';
import { CoreConfigService } from '@core/services/config.service';
import { Injectable, OnInit, OnDestroy } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, Subject, takeUntil } from 'rxjs';

import { environment } from 'environments/environment';
import { AuthenticationService } from 'app/auth/service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  // private
  private language: string;
  private serverLanguages = [
    { code: 'tr', serverCode: 'tr-TR' },
    { code: 'en', serverCode: 'en-US' },
  ];

  /**
   *
   * @param {AuthenticationService} _authenticationService
   */
  constructor(private _authenticationService: AuthenticationService) {}

  /**
   * Add auth header with jwt if user is logged in and request is to api url
   * @param request
   * @param next
   */
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const currentUser = this._authenticationService.currentUserValue;
    const isLoggedIn = currentUser && currentUser.token;
    const localConfig: CoreConfig = JSON.parse(localStorage.getItem('config'));
    const serverLanguageCode = this.serverLanguages.find(
      (x) => x.code === localConfig.app.appLanguage
    ).serverCode;
    //const isApiUrl = request.url.startsWith(environment.apiUrl);
    if (isLoggedIn) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`,
          'Cache-Control': 'no-cache',
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          'Accept-Language': serverLanguageCode ?? currentUser.language,
          Accept: 'application/json',
        },
      });
    }

    return next.handle(request);
  }
}

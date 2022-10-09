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
export class JwtInterceptor implements HttpInterceptor, OnInit, OnDestroy {
  // private
  private _unsubscribeAll: Subject<any>;
  private language: string;

  /**
   *
   * @param {AuthenticationService} _authenticationService
   */
  constructor(
    private _authenticationService: AuthenticationService,
    private _coreConfigService: CoreConfigService
  ) {
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    this._coreConfigService
      .getConfig()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((config: CoreConfig) => {
        this.language =
          config.app.appLanguage +
          '-' +
          config.app.appLanguage.toLocaleUpperCase();
      });
  }

  ngOnDestroy(): void {}

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
    //const isApiUrl = request.url.startsWith(environment.apiUrl);
    if (isLoggedIn) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`,
          'Cache-Control': 'no-cache',
          'Access-Control-Allow-Origin': '*',
          'Content-Type': 'application/json',
          'Accept-Language': this.language ?? currentUser.language,
          Accept: 'application/json',
        },
      });
    }

    return next.handle(request);
  }
}

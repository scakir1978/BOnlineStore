import { CookieService } from "ngx-cookie-service";
import { Injectable } from "@angular/core";
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from "@angular/common/http";
import { Observable } from "rxjs";

import { AuthenticationService } from "../services/auth.service";
import { AuthfakeauthenticationService } from "../services/authfake.service";
import { environment } from "../../../environments/environment";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private authenticationService: AuthenticationService,
    public _cookiesService: CookieService
  ) {}

  private serverLanguages = [
    { code: "tr", serverCode: "tr-TR" },
    { code: "en", serverCode: "en-US" },
  ];

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const currentUser = this.authenticationService.currentUser();
    const userLanguage = this._cookiesService.get("lang");
    const isLoggedIn = currentUser && currentUser.token;

    const serverLanguageCode = this.serverLanguages.find(
      (serverLanguage) => serverLanguage.code === userLanguage
    )?.serverCode;

    if (currentUser && currentUser.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`,
          "Cache-Control": "no-cache",
          "Access-Control-Allow-Origin": "*",
          "Content-Type": "application/json",
          "Accept-Language": serverLanguageCode! ?? currentUser.language!,
          Accept: "application/json",
        },
      });
    }

    return next.handle(request);
  }
}

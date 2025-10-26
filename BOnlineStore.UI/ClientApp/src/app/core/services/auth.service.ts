import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { getFirebaseBackend } from '../../authUtils';
import { User } from '../models/auth.models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { GlobalComponent } from '../../global-component';

import * as oidc from 'oidc-client-ts';
import { AuthenticationScopesEnum } from 'app/base-classes/base-enums/authentication-scopes.enum';

const AUTH_API = GlobalComponent.AUTH_API;

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

@Injectable({ providedIn: 'root' })

/**
 * Auth-service Component
 */
export class AuthenticationService {
  returnUrl: string = '/';

  config: oidc.UserManagerSettings = {
    client_id: 'AngularClient',
    authority: `${environment.identityUrl}`,
    redirect_uri: `${environment.uiUrl}/identity/callback`,
    silent_redirect_uri: `${environment.uiUrl}/identity/silent`,
    post_logout_redirect_uri: `${environment.uiUrl}/identity/callout`,
    response_type: 'code',
    scope: `${AuthenticationScopesEnum.OPENID} ${AuthenticationScopesEnum.PROFILE} ${AuthenticationScopesEnum.DEFINITIONS_FULL_PERMISSION} ${AuthenticationScopesEnum.PRODUCTION_FULL_PERMISSION} ${AuthenticationScopesEnum.GATEWAY_FULL_PERMISSION} ${AuthenticationScopesEnum.BFF_FULL_PERMISSION} ${AuthenticationScopesEnum.IDENTITYSERVERAPI} ${AuthenticationScopesEnum.OFFLINE_ACCESS}`,
    automaticSilentRenew: true,
    response_mode: 'query',
    loadUserInfo: true, // UserInfo endpoint'inden claim'leri çek
  };

  private identityUser: oidc.User | null | undefined = null;
  private userManager: oidc.UserManager;

  user!: User;
  currentUserValue: any;

  private currentUserSubject: BehaviorSubject<User>;
  // public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.userManager = new oidc.UserManager(this.config);
    this.currentUserSubject = new BehaviorSubject<User>(null!);
    //this.currentUser = this.currentUserSubject.asObservable();
  }

  /**
   * Performs the register
   * @param email email
   * @param password password
   */
  register(email: string, first_name: string, password: string) {
    // return getFirebaseBackend()!.registerUser(email, password).then((response: any) => {
    //     const user = response;
    //     return user;
    // });

    // Register Api
    return this.http.post(
      AUTH_API + 'signup',
      {
        email,
        first_name,
        password,
      },
      httpOptions
    );
  }

  /**
   * Performs the auth
   * @param email email of user
   * @param password password of user
   */
  login(email: string, password: string) {
    // return getFirebaseBackend()!.loginUser(email, password).then((response: any) => {
    //     const user = response;
    //     return user;
    // });

    return this.http.post(
      AUTH_API + 'signin',
      {
        email,
        password,
      },
      httpOptions
    );
  }

  /**
   * Returns the current user
   */
  public currentUser(): User {
    //return getFirebaseBackend()!.getAuthenticatedUser();
    return this.currentUserSubject.value;
  }

  /**
   * Returns the current user as Observable
   */
  public currentUser$(): Observable<User> {
    return this.currentUserSubject.asObservable();
  }

  /**
   * Logout the user
   */
  logout() {
    // logout the user
    // return getFirebaseBackend()!.logout();
    this.currentUserSubject.next(null!);
  }

  /**
   * Reset password
   * @param email email
   */
  resetPassword(email: string) {
    return getFirebaseBackend()!
      .forgetPassword(email)
      .then((response: any) => {
        const message = response.data;
        return message;
      });
  }

  completeAuthentication(): Promise<oidc.User> {
    return this.userManager
      .signinRedirectCallback()
      .then(async (identityUser) => {
        // UserInfo endpoint'inden ek claim'leri al
        /*try {
        const userInfo = await this.userManager.getUser();
        if (userInfo) {
          // UserInfo'dan gelen claim'leri identityUser.profile'a ekle
          Object.assign(identityUser.profile, userInfo.profile);
        }
      } catch (error) {
        console.warn('UserInfo alınamadı:', error);
      }*/

        this.createUIUser(identityUser);
        return identityUser;
      });
  }

  loginIndetity(returnUrl: string = '/'): void {
    this.returnUrl = returnUrl;
    this.userManager.signinRedirect();
  }

  logoutIndetity(): void {
    this.currentUserSubject.next(null!);
    // Pass locale to IdentityServer using OIDC ui_locales and ASP.NET Core culture params
    const locale = this.getPreferredLocale();
    this.userManager.signoutRedirect({
      extraQueryParams: {
        ui_locales: locale,
        culture: locale,
        'ui-culture': locale,
      },
    });
  }

  silentRefresh(): Promise<void> {
    return this.userManager.signinSilentCallback();
  }

  signoutRedirectCallback() {
    return this.userManager.signoutRedirectCallback();
  }

  /**
   * Determines the preferred UI locale to send to IdentityServer.
   * Priority: current user language -> cookie 'locale' -> environment default ('tr-TR').
   * Ensures format like 'tr-TR' when only language code (e.g., 'tr') is available.
   */
  private getPreferredLocale(): string {
    let serverLanguages = [
      { code: 'tr', serverCode: 'tr-TR' },
      { code: 'en', serverCode: 'en-US' },
    ];

    const userLanguage = localStorage.getItem('locale') || 'tr';

    const serverLanguageCode = serverLanguages.find(
      (serverLanguage) => serverLanguage.code === userLanguage
    )?.serverCode;

    return serverLanguageCode ?? 'tr-TR';
  }

  private createUIUser(identityUser: oidc.User) {
    // Geliştirme ortamında debug bilgileri
    if (!environment.production) {
      console.log('Identity User Profile:', identityUser.profile);
      console.log('Locale claim:', identityUser.profile?.locale);
    }

    var user: User = new User();

    user.id = identityUser.profile.sid;
    //user.avatar = 'avatar-s-11.jpg';
    user.email = identityUser.profile.email;
    user.firstName = identityUser.profile.given_name;
    user.lastName = identityUser.profile.family_name;
    user.userName = identityUser.profile.preferred_username;
    user.nickname = identityUser.profile.nickname;
    //user.role = Role.Admin;
    user.token = identityUser.access_token;
    user.language = identityUser.profile.locale ?? 'tr-TR';

    if (!localStorage.getItem('locale')) {
      localStorage.setItem('locale', user.language.split('-')[0]);
    }

    this.currentUserSubject.next(user);
  }

  /***
   * Dil değişikliği sonrası web sayfası tekrar yüklendiğinden
   * oturum devamlılığı sağlanması için bu kod eklenmiştir.
   * Burada user bilgisini identityServer'dan almaya çalışır.
   * Eğer oturum geçerliyse kullanıcı bilgilerini alır.
   * */
  async initAuthPersistence(): Promise<void> {
    try {
      const existing = await this.userManager.getUser();
      if (existing && !existing.expired) {
        this.createUIUser(existing);
        return;
      }
    } catch {
      // Hata alırsa herhangi bir işlem yapma
    }
  }
}

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
  };

  private identityUser: oidc.User | null | undefined = null;
  private userManager: oidc.UserManager;

  user!: User;
  currentUserValue: any;

  private currentUserSubject: BehaviorSubject<User>;
  // public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
    this.userManager = new oidc.UserManager(this.config);
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser')!)
    );
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
   * Logout the user
   */
  logout() {
    // logout the user
    // return getFirebaseBackend()!.logout();
    localStorage.removeItem('currentUser');
    localStorage.removeItem('token');
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
    return this.userManager.signinRedirectCallback().then((identityUser) => {
      this.createUIUser(identityUser);
      return identityUser;
    });
  }

  loginIndetity(returnUrl: string = '/'): void {
    this.returnUrl = returnUrl;
    this.userManager.signinRedirect();
  }

  logoutIndetity(): void {
    localStorage.removeItem('currentUser');
    // notify
    this.currentUserSubject.next(null!);

    this.userManager.signoutRedirect();
  }

  silentRefresh(): Promise<void> {
    return this.userManager.signinSilentCallback();
  }

  signoutRedirectCallback() {
    return this.userManager.signoutRedirectCallback();
  }

  private createUIUser(identityUser: oidc.User) {
    var user: User = new User();

    user.id = identityUser.profile.sid;
    //user.avatar = 'avatar-s-11.jpg';
    user.email = identityUser.profile.email;
    user.firstName = identityUser.profile.name;
    user.lastName = identityUser.profile.family_name;
    //user.role = Role.Admin;
    user.token = identityUser.access_token;
    user.language = identityUser.profile.locale ?? 'tr-TR';

    localStorage.setItem('currentUser', JSON.stringify(user));

    this.currentUserSubject.next(user);
  }
}

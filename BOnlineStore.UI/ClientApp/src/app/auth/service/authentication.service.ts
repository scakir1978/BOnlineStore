import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'environments/environment';
import { User, Role } from 'app/auth/models';
import { ToastrService } from 'ngx-toastr';
import * as oidc from 'oidc-client-ts';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  returnUrl: string = '/';

  config: oidc.UserManagerSettings = {
    client_id: 'AngularClient',
    authority: `${environment.identityUrl}`,
    //authority: 'https://localhost:5001',
    redirect_uri: `${environment.uiUrl}/pages/callback`,
    //redirect_uri: "http://localhost:4200/pages/callback",
    silent_redirect_uri: `${environment.uiUrl}/pages/silent`,
    //silent_redirect_uri: "http://localhost:4200/pages/silent",
    post_logout_redirect_uri: `${environment.uiUrl}/pages/callout`,
    //post_logout_redirect_uri: "http://localhost:4200/pages/callout",
    response_type: 'code',
    /*metadata: {
      authorization_endpoint: `${environment.identityUrl}/connect/authorize`,
      token_endpoint: `${environment.identityUrl}/connect/token`,
      userinfo_endpoint: `${environment.identityUrl}/connect/userinfo`,
      end_session_endpoint: `${environment.identityUrl}/connect/endsession`,
      check_session_iframe: `${environment.identityUrl}/connect/checksession`,
      revocation_endpoint: `${environment.identityUrl}/connect/revocation`,
      introspection_endpoint: `${environment.identityUrl}/connect/introspect`,
    },*/
    scope:
      'openid profile definitions_full_permission production_full_permission gateway_full_permission IdentityServerApi offline_access',
    automaticSilentRenew: true,
    response_mode: 'query',
  };

  private identityUser: oidc.User | null | undefined = null;
  private userManager: oidc.UserManager;

  //public
  public currentUser: Observable<User>;

  //private
  private currentUserSubject: BehaviorSubject<User>;

  /**
   *
   * @param {HttpClient} _http
   * @param {ToastrService} _toastrService
   */
  constructor(
    private _http: HttpClient,
    private _toastrService: ToastrService
  ) {
    this.userManager = new oidc.UserManager(this.config);

    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  // getter: currentUserValue
  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  /**
   *  Confirms if user is admin
   */
  get isAdmin() {
    return (
      this.currentUser && this.currentUserSubject.value.role === Role.Admin
    );
  }

  /**
   *  Confirms if user is client
   */
  get isClient() {
    return (
      this.currentUser && this.currentUserSubject.value.role === Role.Client
    );
  }

  private createUIUser(identityUser: oidc.User) {
    var user: User = new User();

    user.id = identityUser.profile.sid;
    user.avatar = 'avatar-s-11.jpg';
    user.email = identityUser.profile.email;
    user.firstName = identityUser.profile.name;
    user.lastName = identityUser.profile.family_name;
    user.role = Role.Admin;
    user.token = identityUser.access_token;
    user.language = identityUser.profile.locale ?? 'tr-TR';

    localStorage.setItem('currentUser', JSON.stringify(user));

    this.currentUserSubject.next(user);
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
    this.currentUserSubject.next(null);

    this.userManager.signoutRedirect();
  }

  silentRefresh(): Promise<void> {
    return this.userManager.signinSilentCallback();
  }

  signoutRedirectCallback() {
    return this.userManager.signoutRedirectCallback();
  }

  /**
   * User login
   *
   * @param email
   * @param password
   * @returns user
   */
  login(email: string, password: string) {
    return this._http
      .post<any>(`${environment.identityUrl}/users/authenticate`, {
        email,
        password,
      })
      .pipe(
        map((user) => {
          // login successful if there's a jwt token in the response
          if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));

            // Display welcome toast!
            setTimeout(() => {
              this._toastrService.success(
                'You have successfully logged in as an ' +
                  user.role +
                  ' user to Vuexy. Now you can start to explore. Enjoy! 🎉',
                '👋 Welcome, ' + user.firstName + '!',
                { toastClass: 'toast ngx-toastr', closeButton: true }
              );
            }, 2500);

            // notify
            this.currentUserSubject.next(user);
          }

          return user;
        })
      );
  }

  /**
   * User logout
   *
   */
  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    // notify
    this.currentUserSubject.next(null);
  }
}

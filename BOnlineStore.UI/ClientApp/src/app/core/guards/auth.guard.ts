import { Injectable } from "@angular/core";
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";

// Auth Services
import { AuthenticationService } from "../services/auth.service";

@Injectable({ providedIn: "root" })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const currentUser = this.authenticationService.currentUser();

    if (currentUser) {
      if (
        route.data["roles"] &&
        route.data["roles"].indexOf(currentUser.role) === -1
      ) {
        // role not authorised so redirect to not-authorized page
        this.router.navigate(["/auth/errors/page-401"]);
        return false;
      }
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    /*this.router.navigate(["/auth/login"], {
      queryParams: { returnUrl: state.url },
    });*/

    this.authenticationService.loginIndetity();

    return false;
  }
}

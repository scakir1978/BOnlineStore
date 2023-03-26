import { AuthenticationService } from './../core/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.scss'],
})
export class CallbackComponent implements OnInit {
  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    if (this.router.url.includes('callback')) {
      this.authService.completeAuthentication().then((user) => {
        this.router.navigateByUrl('/');
      });
    }
    if (this.router.url.includes('callout')) {
      this.authService
        .signoutRedirectCallback()
        .then(() => this.router.navigateByUrl('/'));
    }
    if (this.router.url.includes('silent')) {
      this.authService
        .silentRefresh()
        .then(() => this.router.navigateByUrl('/'));
    }
  }
}

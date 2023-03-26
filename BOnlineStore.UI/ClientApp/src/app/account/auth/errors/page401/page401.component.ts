import { AuthenticationService } from './../../../../core/services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-page401',
  templateUrl: './page401.component.html',
  styleUrls: ['./page401.component.scss'],
})

/**
 * Page500 Component
 */
export class Page401Component implements OnInit {
  constructor(private authenticationService: AuthenticationService) {}

  ngOnInit(): void {}

  gotoLoginPage() {
    this.authenticationService.loginIndetity();
  }
}

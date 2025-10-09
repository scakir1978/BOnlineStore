import { LayoutService } from './../../core/services/layout.service';
import { Component, OnInit, EventEmitter, Output, Inject, OnDestroy } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { EventService } from '../../core/services/event.service';
import { Subscription } from 'rxjs';

//Logout
import { environment } from '../../../environments/environment';
import { AuthenticationService } from '../../core/services/auth.service';
import { AuthfakeauthenticationService } from '../../core/services/authfake.service';
import { Router } from '@angular/router';

// Language
import { CookieService } from 'ngx-cookie-service';
import { LanguageService } from '../../core/services/language.service';
import { TranslateService } from '@ngx-translate/core';

import { CartModel } from './topbar.model';
import { cartData } from './data';

import themes from 'devextreme/ui/themes';
import { locale, loadMessages } from 'devextreme/localization';
import * as trDevextremeMessages from '../../../assets/i18n/devextreme/tr.json';
import * as enDevextremeMessages from '../../../assets/i18n/devextreme/en.json';

themes.current(window.localStorage.getItem('dx-theme') || 'dark');

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss'],
})
export class TopbarComponent implements OnInit, OnDestroy {
  element: any;
  mode: string | undefined;
  @Output() mobileMenuButtonClicked = new EventEmitter();

  cartData!: CartModel[];
  total = 0;
  cart_length: any = 0;

  flagvalue: any;
  valueset: any;
  countryName: any;
  cookieValue: any;
  userData: any;

  currentTheme: string = 'light';
  private themeSubscription: any;
  private userSubscription: Subscription = new Subscription();

  constructor(
    @Inject(DOCUMENT) private document: any,
    private eventService: EventService,
    public languageService: LanguageService,
    public _cookiesService: CookieService,
    public translate: TranslateService,
    private authService: AuthenticationService,
    private authFackservice: AuthfakeauthenticationService,
    private router: Router,
    private layoutService: LayoutService
  ) {
    //devextreme localization
    loadMessages(trDevextremeMessages);
    loadMessages(enDevextremeMessages);
  }

  ngOnInit(): void {
    this.element = document.documentElement;

    // User data'yı observable olarak subscribe et
    this.userSubscription = this.authService.currentUser$().subscribe(user => {
      this.userData = user;
    });

    // Cookies wise Language set
    this.cookieValue = this._cookiesService.get('lang');
    const val = this.listLang.filter((x) => x.lang === this.cookieValue);
    this.countryName = val.map((element) => element.text);
    if (val.length === 0) {
      if (this.flagvalue === undefined) {
        this.valueset = 'assets/images/flags/tr.svg';
      }
    } else {
      this.flagvalue = val.map((element) => element.flag);
    }

    //devextreme localization
    locale(this.cookieValue || 'tr');
    sessionStorage.setItem('locale', this.cookieValue || 'tr');

    //  Fetch Data
    this.cartData = cartData;
    this.cart_length = this.cartData.length;
    this.cartData.forEach((item) => {
      var item_price = item.quantity * item.price;
      this.total += item_price;
    });
  }

  ngOnDestroy(): void {
    // Subscription'ı temizle
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }

  /**
   * Toggle the menu bar when having mobile screen
   */
  toggleMobileMenu(event: any) {
    document.querySelector('.hamburger-icon')?.classList.toggle('open');
    event.preventDefault();
    this.mobileMenuButtonClicked.emit();
  }

  /**
   * Fullscreen method
   */
  fullscreen() {
    document.body.classList.toggle('fullscreen-enable');
    if (
      !document.fullscreenElement &&
      !this.element.mozFullScreenElement &&
      !this.element.webkitFullscreenElement
    ) {
      if (this.element.requestFullscreen) {
        this.element.requestFullscreen();
      } else if (this.element.mozRequestFullScreen) {
        /* Firefox */
        this.element.mozRequestFullScreen();
      } else if (this.element.webkitRequestFullscreen) {
        /* Chrome, Safari and Opera */
        this.element.webkitRequestFullscreen();
      } else if (this.element.msRequestFullscreen) {
        /* IE/Edge */
        this.element.msRequestFullscreen();
      }
    } else {
      if (this.document.exitFullscreen) {
        this.document.exitFullscreen();
      } else if (this.document.mozCancelFullScreen) {
        /* Firefox */
        this.document.mozCancelFullScreen();
      } else if (this.document.webkitExitFullscreen) {
        /* Chrome, Safari and Opera */
        this.document.webkitExitFullscreen();
      } else if (this.document.msExitFullscreen) {
        /* IE/Edge */
        this.document.msExitFullscreen();
      }
    }
  }

  /**
   * Topbar Light-Dark Mode Change
   */
  changeMode(mode: string) {
    this.mode = mode;
    this.layoutService.setLayoutModeFromTopbar(mode);
    this.getLogoPath();

    switch (mode) {
      case 'light':
        document.documentElement.setAttribute('data-bs-theme', 'light');
        window.localStorage.setItem('dx-theme', 'light');
        themes.current('light');
        break;
      case 'dark':
        document.documentElement.setAttribute('data-bs-theme', 'dark');
        window.localStorage.setItem('dx-theme', 'dark');
        themes.current('dark');
        break;
      default:
        document.documentElement.setAttribute('data-bs-theme', 'light');
        window.localStorage.setItem('dx-theme', 'light');
        themes.current('light');
        break;
    }

    this.eventService.broadcast('changeMode', mode);
  }

  /***
   * Language Listing
   */
  listLang = [
    { text: 'Türkçe', flag: 'assets/images/flags/tr.svg', lang: 'tr' },
    { text: 'English', flag: 'assets/images/flags/us.svg', lang: 'en' },
  ];

  /***
   * Language Value Set
   */
  setLanguage(text: string, lang: string, flag: string) {
    this.countryName = text;
    this.flagvalue = flag;
    this.cookieValue = lang;

    sessionStorage.setItem('locale', lang);
    this.languageService.setLanguage(lang);
    locale(lang);

    window.location.reload();
  }

  /**
   * Logout the user
   */
  logout() {
    this.authService.logoutIndetity();
    // if (environment.defaultauth === 'firebase') {
    //   this.authService.logout();
    // } else {
    //   this.authFackservice.logout();
    // }
    //this.router.navigate(["/auth/login"]);
  }

  windowScroll() {
    // Top Btn Set
    if (
      document.body.scrollTop > 100 ||
      document.documentElement.scrollTop > 100
    ) {
      (document.getElementById('back-to-top') as HTMLElement).style.display =
        'block';
      document.getElementById('page-topbar')?.classList.add('topbar-shadow');
    } else {
      (document.getElementById('back-to-top') as HTMLElement).style.display =
        'none';
      document.getElementById('page-topbar')?.classList.remove('topbar-shadow');
    }
  }

  // Delete Item
  deleteItem(event: any, id: any) {
    var price = event.target
      .closest('.dropdown-item')
      .querySelector('.item_price').innerHTML;
    var Total_price = this.total - price;
    this.total = Total_price;
    this.cart_length = this.cart_length - 1;
    this.total > 1
      ? ((document.getElementById('empty-cart') as HTMLElement).style.display =
          'none')
      : ((document.getElementById('empty-cart') as HTMLElement).style.display =
          'block');
    document.getElementById('item_' + id)?.remove();
  }

  /**
   * Get logo path based on current theme
   */
  getLogoPath(): string {
    return this.mode === 'dark'
      ? 'assets/images/logo-console-log-dark.png'
      : 'assets/images/logo-console-log-light.png';
  }
}

import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({ providedIn: 'root' })
export class LanguageService {
  public languages: string[] = ['en', 'tr'];

  constructor(public translate: TranslateService) {
    let browserLang: any;
    /***
     * cookie Language Get
     */
    this.translate.addLangs(this.languages);
    if (localStorage.getItem('locale')) {
      browserLang = localStorage.getItem('locale');
    } else {
      browserLang = translate.getBrowserLang();
    }
    translate.use(browserLang.match(/en|tr/) ? browserLang : 'tr');
  }

  /***
   * Cookie Language set
   */
  public setLanguage(lang: any) {
    this.translate.use(lang);
    localStorage.setItem('locale', lang);
  }
}

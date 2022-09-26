import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class AccountSettingsService implements Resolve<any> {

  accountSettinsData: any = {
    accountSetting: {
      general: {
        avatar: 'assets/images/portrait/small/avatar-s-11.jpg',
        username: 'johndoe',
        fullName: 'John Doe',
        email: 'granger007@hogward.com',
        company: 'IBM Technology'
      },
      info: {
        bio: '',
        dob: null,
        country: 'USA',
        website: '',
        phone: '(+656) 254 2568'
      },
      social: {
        socialLinks: {
          twitter: 'https://www.twitter.com',
          facebook: '',
          google: '',
          linkedIn: 'https://www.linkedin.com',
          instagram: '',
          quora: ''
        },
        connections: {
          twitter: {
            profileImg: 'assets/images/avatars/11-small.png',
            id: '@johndoe'
          },
          google: {
            profileImg: 'assets/images/avatars/3-small.png',
            id: '@luraweber'
          },
          facebook: {},
          github: {}
        }
      },
      notification: {
        commentOnArticle: true,
        AnswerOnForm: true,
        followMe: false,
        newAnnouncements: true,
        productUpdates: true,
        blogDigest: false
      }
    }
  };
  
  rows: any;
  onSettingsChanged: BehaviorSubject<any>;

  /**
   * Constructor
   *
   * @param {HttpClient} _httpClient
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onSettingsChanged = new BehaviorSubject({});
  }

  /**
   * Resolver
   *
   * @param {ActivatedRouteSnapshot} route
   * @param {RouterStateSnapshot} state
   * @returns {Observable<any> | Promise<any> | any}
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getDataTableRows()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get rows
   */
  getDataTableRows(): Promise<any[]> {
    return new Promise((resolve, reject) => {
      this.rows = this.accountSettinsData;
      this.onSettingsChanged.next(this.rows);
      resolve(this.rows);      
    });
  }
}

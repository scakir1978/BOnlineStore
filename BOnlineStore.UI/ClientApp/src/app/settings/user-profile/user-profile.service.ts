import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { Observable } from 'rxjs';
import {
  UserProfileDto,
  UserProfileUpdateDto,
} from '../../dtos/settings/user-profile.dto';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService extends BaseService {
  private readonly identityApiUrl = environment.identityUrl + '/api/User';

  constructor(public override _http: HttpClient) {
    super(_http, environment.identityUrl, 'User');
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getUserByEmail(email: string): Observable<UserProfileDto> {
    return this._http.get<UserProfileDto>(
      `${this.identityApiUrl}/by-email/${encodeURIComponent(email)}`
    );
  }

  updateUserProfile(
    userProfile: UserProfileUpdateDto
  ): Observable<UserProfileDto> {
    return this._http.put<UserProfileDto>(
      `${this.identityApiUrl}`,
      userProfile
    );
  }

  getUserById(id: string): Observable<UserProfileDto> {
    return this._http.get<UserProfileDto>(`${this.identityApiUrl}/${id}`);
  }
}

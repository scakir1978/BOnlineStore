import { SettingsControllerNamesEnum } from '../../base-classes/base-enums/settings-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import CustomStore from 'devextreme/data/custom_store';
import { lastValueFrom, Observable, switchAll } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserRoleService extends BaseService {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.identityUrl + '/api/',
      SettingsControllerNamesEnum.USERROLE,
    );
  }

  getDataSource(): DataSource {
    return super.getBaseDataSource(
      environment.identityUrl + '/api/',
      SettingsControllerNamesEnum.USERROLE,
      ['userId', 'roleId'],
    );
  }

  getRoleDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.identityUrl + '/api/',
      SettingsControllerNamesEnum.ROLE,
    );
  }

  getUserDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.identityUrl + '/api/',
      SettingsControllerNamesEnum.USER,
    );
  }
}

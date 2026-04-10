import { SettingsControllerNamesEnum } from '../../base-classes/base-enums/settings-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { lastValueFrom, Observable, switchAll } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoleService extends BaseService {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.identityUrl + '/api/',
      SettingsControllerNamesEnum.ROLE,
    );
  }

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }
}

import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { DefinitionsControllerNamesEnum } from '../../base-classes/base-enums/definitions-controller-names.enum';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import DataSource from 'devextreme/data/data_source';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ModelGroupService extends BaseService  {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MODELGROUP
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }
}

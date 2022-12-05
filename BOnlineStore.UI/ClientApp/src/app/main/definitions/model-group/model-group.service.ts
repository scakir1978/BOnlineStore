import { DefinitionControllerNamesEnum } from './../../base-classes/base-enums/definition-controller-names.enum';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { BaseService } from 'app/main/base-classes/base-services/base-service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ModelGroupService extends BaseService implements Resolve<any> {
  constructor(public _http: HttpClient) {
    super(_http, DefinitionControllerNamesEnum.MODELGROUP);
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }
}

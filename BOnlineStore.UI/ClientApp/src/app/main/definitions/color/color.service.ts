import { DefinitionControllerNamesEnum } from './../../base-classes/base-enums/definition-controller-names.enum';
import CustomStore from 'devextreme/data/custom_store';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { BaseService } from 'app/main/base-classes/base-services/base-service';
import DataSource from 'devextreme/data/data_source';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ColorService extends BaseService implements Resolve<any> {
  constructor(public _http: HttpClient) {
    super(_http, DefinitionControllerNamesEnum.COLOR);
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getColorGroupDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      DefinitionControllerNamesEnum.COLORGROUP
    );
  }
}

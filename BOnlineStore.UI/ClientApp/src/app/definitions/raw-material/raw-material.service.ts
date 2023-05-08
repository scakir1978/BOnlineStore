import { DefinitionsControllerNamesEnum } from '../../base-classes/base-enums/definitions-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { lastValueFrom, Observable, switchAll } from 'rxjs';
import CustomStore from 'devextreme/data/custom_store';

@Injectable({
  providedIn: 'root',
})
export class RawMaterialService extends BaseService implements Resolve<any> {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.RAWMATERIAL
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getUnitDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.UNIT
    );
  }

  getRawMaterialGroupDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.RAWMATERIALGROUP
    );
  }

  getRawMaterialMasterGroupDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.RAWMATERIAL
    );
  }
}

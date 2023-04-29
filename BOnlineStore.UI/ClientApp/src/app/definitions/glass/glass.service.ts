import { DefinitionsControllerNamesEnum } from '../../base-classes/base-enums/definitions-controller-names.enum';
import CustomStore from 'devextreme/data/custom_store';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import DataSource from 'devextreme/data/data_source';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GlassService extends BaseService implements Resolve<any> {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.GLASS
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getGlassGroupDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.COLORGROUP
    );
  }
}

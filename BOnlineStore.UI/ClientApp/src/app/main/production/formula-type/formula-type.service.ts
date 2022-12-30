import { ProductionControllerNamesEnum } from '../../base-classes/base-enums/production-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from 'environments/environment';
import { BaseService } from 'app/main/base-classes/base-services/base-service';
import { lastValueFrom, Observable, switchAll } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FormulaTypeService extends BaseService implements Resolve<any> {
  constructor(public _http: HttpClient) {
    super(
      _http,
      environment.productionUrl,
      ProductionControllerNamesEnum.FORMULATYPE
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

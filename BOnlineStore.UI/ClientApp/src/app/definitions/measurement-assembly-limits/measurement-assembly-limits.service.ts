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
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class MeasurementAssemblyLimitsService
  extends BaseService
  implements Resolve<any>
{
  constructor(
    public override _http: HttpClient,
    public _translate: TranslateService
  ) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MEASUREMENTASSEMBLYLIMITS
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getAssemblerDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.ASSEMBLER
    );
  }

  getRegionDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.REGION
    );
  }

  getDaysDataSource(): Observable<any> {
    /*return [
      { id: 1, name: this._translate.instant('MONDAY') },
      { id: 2, name: this._translate.instant('TUESDAY') },
      { id: 3, name: this._translate.instant('WEDNESDAY') },
      { id: 4, name: this._translate.instant('THURSDAY') },
      { id: 5, name: this._translate.instant('FRIDAY') },
      { id: 6, name: this._translate.instant('SATURDAY') },
      { id: 7, name: this._translate.instant('SUNDAY') },
    ];*/

    return this._translate.get([
      'MONDAY',
      'TUESDAY',
      'WEDNESDAY',
      'THURSDAY',
      'FRIDAY',
      'SATURDAY',
      'SUNDAY',
    ]);
  }
}

export class Day {
  id: number;
  name: string;
}

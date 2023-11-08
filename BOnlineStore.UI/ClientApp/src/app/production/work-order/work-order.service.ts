import { ProductionControllerNamesEnum } from '../../base-classes/base-enums/production-controller-names.enum';
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
import { DefinitionsControllerNamesEnum } from 'app/base-classes/base-enums/definitions-controller-names.enum';
import { WorkOrderStatus } from './models/work-order-status';
import { WorkOrderStatusEnum } from './enums/work-order-status.enum';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class WorkOrderService extends BaseService implements Resolve<any> {
  constructor(
    public override _http: HttpClient,
    public _translate: TranslateService
  ) {
    super(
      _http,
      environment.productionUrl,
      ProductionControllerNamesEnum.WORKORDER
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getRawModelDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MODEL,
      'LoadForCombo'
    );
  }

  getRawColorDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.COLOR
    );
  }

  getRawGlassDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.GLASS
    );
  }

  getRawFirmDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.FIRM,
      'LoadForCombo'
    );
  }

  getRawTemplateDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.TEMPLATE
    );
  }

  getFormulaVariableTypes(): WorkOrderStatus[] {
    var workOrderStatus: WorkOrderStatus[] = [
      {
        code: WorkOrderStatusEnum.ASSEMBLY,
        name: this._translate.instant(WorkOrderStatusEnum.ASSEMBLY),
      },
      {
        code: WorkOrderStatusEnum.CARGO,
        name: this._translate.instant(WorkOrderStatusEnum.CARGO),
      },
    ];

    return workOrderStatus;
  }
}

import { ICodeName } from './../../base-classes/base-interfaces/code-name-interface';
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
import { SwingDirectionEnum } from './enums/swing-direction.enum';

@Injectable({
  providedIn: 'root',
})
export class WorkOrderService extends BaseService implements Resolve<any> {
  constructor(
    public override _http: HttpClient,
    private _translate: TranslateService
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

  getWorkOrderStatusList(): ICodeName[] {
    console.log(this._translate.instant('ASSEMBLY'));
    var workOrderStatusList: ICodeName[] = [
      {
        code: WorkOrderStatusEnum.ASSEMBLY,
        name: this._translate.instant('ASSEMBLY'),
      },
      {
        code: WorkOrderStatusEnum.CARGO,
        name: this._translate.instant('CARGO'),
      },
      {
        code: WorkOrderStatusEnum.DEALERDELIVERY,
        name: this._translate.instant('DEALERDELIVERY'),
      },
      {
        code: WorkOrderStatusEnum.FACTORYFINISHED,
        name: this._translate.instant('FACTORYFINISHED'),
      },
    ];

    return workOrderStatusList;
  }

  getSwingDirectionList(): ICodeName[] {
    var swingDirectionList: ICodeName[] = [
      {
        code: SwingDirectionEnum.LEFTHANDED,
        name: this._translate.instant('LEFTHANDED'),
      },
      {
        code: SwingDirectionEnum.NONE,
        name: this._translate.instant('NONE'),
      },
      {
        code: SwingDirectionEnum.RIGHTHANDED,
        name: this._translate.instant('RIGHTHANDED'),
      },
    ];

    return swingDirectionList;
  }
}

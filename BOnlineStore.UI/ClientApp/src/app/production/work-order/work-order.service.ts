import { WorkOrderFormFrontEndDto } from './../../dtos/production/work-order-form-front-end-interface';
import { ProductionControllerNamesEnum } from '../../base-classes/base-enums/production-controller-names.enum';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { Observable } from 'rxjs';
import CustomStore from 'devextreme/data/custom_store';
import { DefinitionsControllerNamesEnum } from 'app/base-classes/base-enums/definitions-controller-names.enum';
import { BffControllerNamesEnum } from 'app/base-classes/base-enums/bff-controller-names.enum';

@Injectable({
  providedIn: 'root',
})
export class WorkOrderService extends BaseService implements Resolve<any> {
  constructor(public override _http: HttpClient) {
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

  getRawDeliveryAdressDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.DELIVERYADRESS
    );
  }

  getCountryDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.COUNTRY
    );
  }

  getCityDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.CITY
    );
  }

  getCountyDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.COUNTY
    );
  }

  getDistrictDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.DISTRICT
    );
  }

  calculateProductionList(workOrderId: string): Promise<Object> {
    return this.httpGetRequest(
      'CalculateProductionList',
      new HttpParams().append('workOrderId', workOrderId)
    );
  }

  calculateProductionListBff(
    workOrderId: string
  ): Promise<WorkOrderFormFrontEndDto> {
    return this.httpGetRequestGeneric<WorkOrderFormFrontEndDto>(
      'CalculateProductionList',
      new HttpParams().append('workOrderId', workOrderId),
      BffControllerNamesEnum.ONLINESTORE,
      environment.bffUrl
    );
  }
}

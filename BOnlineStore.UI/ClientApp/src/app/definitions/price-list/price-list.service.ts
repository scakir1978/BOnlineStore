import { DefinitionsControllerNamesEnum } from '../../base-classes/base-enums/definitions-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { lastValueFrom, Observable, switchAll } from 'rxjs';
import CustomStore from 'devextreme/data/custom_store';
import { PriceListMaster } from './models/price-list-form-model';
import { HttpRequestMethodsEnum } from 'app/base-classes/base-enums/http-request-methods.enum';

@Injectable({
  providedIn: 'root',
})
export class PriceListService extends BaseService {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.PRICELIST
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getColorDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.COLOR
    );
  }

  getGlassDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.GLASS
    );
  }

  getCurrencyDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.CURRENCY
    );
  }

  getModelDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MODEL,
      'LoadForCombo'
    );
  }

  priceListInsert(priceListData: PriceListMaster) {
    this.sendRequest(
      environment.definitionsUrl +
        DefinitionsControllerNamesEnum.PRICELIST +
        '/Insert',
      HttpRequestMethodsEnum.INSERT,
      '',
      priceListData
    );
  }

  priceListUpdate(priceListData: PriceListMaster, key: string) {
    this.sendRequest(
      environment.definitionsUrl +
        DefinitionsControllerNamesEnum.PRICELIST +
        '/Update',
      HttpRequestMethodsEnum.UPDATE,
      key,
      priceListData
    );
  }

  priceListDelete(key: string) {
    this.sendRequest(
      environment.definitionsUrl +
        DefinitionsControllerNamesEnum.PRICELIST +
        '/Update',
      HttpRequestMethodsEnum.DELETE,
      key
    );
  }
}

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
  constructor(public _http: HttpClient) {
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

  async insert(priceListData: PriceListMaster): Promise<any> {
    return await this.sendRequest(
      environment.definitionsUrl + DefinitionsControllerNamesEnum.PRICELIST,
      HttpRequestMethodsEnum.INSERT,
      '',
      priceListData
    );
  }

  async update(priceListData: PriceListMaster, key: string): Promise<any> {
    return await this.sendRequest(
      environment.definitionsUrl + DefinitionsControllerNamesEnum.PRICELIST,
      HttpRequestMethodsEnum.UPDATE,
      key,
      priceListData
    );
  }

  delete(key: string) {
    this.sendRequest(
      environment.definitionsUrl + DefinitionsControllerNamesEnum.PRICELIST,
      HttpRequestMethodsEnum.DELETE,
      key
    );
  }

  async getById(key: string): Promise<any> {
    return await this.sendRequest(
      environment.definitionsUrl + DefinitionsControllerNamesEnum.PRICELIST,
      HttpRequestMethodsEnum.GETBYID,
      key
    );
  }
}

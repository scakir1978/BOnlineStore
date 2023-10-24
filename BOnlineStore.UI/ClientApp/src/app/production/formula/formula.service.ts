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
import { Formula } from './models/formula-form-model';
import { HttpRequestMethodsEnum } from 'app/base-classes/base-enums/http-request-methods.enum';

@Injectable({
  providedIn: 'root',
})
export class FormulaService extends BaseService {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.productionUrl,
      ProductionControllerNamesEnum.FORMULA
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getModelDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MODEL,
      'LoadForCombo'
    );
  }

  getRawMaterialDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.RAWMATERIAL,
      'LoadForCombo'
    );
  }

  getFormulaTypeDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.productionUrl,
      ProductionControllerNamesEnum.FORMULATYPE
    );
  }

  async insert(formulaData: Formula): Promise<any> {
    return await this.sendRequest(
      environment.productionUrl + ProductionControllerNamesEnum.FORMULA,
      HttpRequestMethodsEnum.INSERT,
      '',
      formulaData
    );
  }

  async update(formulaData: Formula, key: string): Promise<any> {
    return await this.sendRequest(
      environment.productionUrl + ProductionControllerNamesEnum.FORMULA,
      HttpRequestMethodsEnum.UPDATE,
      key,
      formulaData
    );
  }

  delete(key: string) {
    this.sendRequest(
      environment.productionUrl + ProductionControllerNamesEnum.FORMULA,
      HttpRequestMethodsEnum.DELETE,
      key
    );
  }

  async getById(key: string): Promise<any> {
    return await this.sendRequest(
      environment.productionUrl + ProductionControllerNamesEnum.FORMULA,
      HttpRequestMethodsEnum.GETBYID,
      key
    );
  }
}

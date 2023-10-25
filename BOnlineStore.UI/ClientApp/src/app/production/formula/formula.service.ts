import { ProductionControllerNamesEnum } from '../../base-classes/base-enums/production-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { Observable } from 'rxjs';
import CustomStore from 'devextreme/data/custom_store';
import { DefinitionsControllerNamesEnum } from 'app/base-classes/base-enums/definitions-controller-names.enum';
import { Formula } from './models/formula-form-model';
import { HttpRequestMethodsEnum } from 'app/base-classes/base-enums/http-request-methods.enum';
import { FormulaVariableTypes } from './models/formula-variable-types';
import { TranslateService } from '@ngx-translate/core';
import { IFormulaDetail } from './models/formula-form-interface';

@Injectable({
  providedIn: 'root',
})
export class FormulaService extends BaseService {
  constructor(
    public override _http: HttpClient,
    public _translate: TranslateService
  ) {
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

  getRawModelDataSource(): CustomStore {
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

  getRawFormulaTypeDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.productionUrl,
      ProductionControllerNamesEnum.FORMULATYPE
    );
  }

  getRawFormulaDataSource(): CustomStore {
    var ds = super.getBaseRawCustomStore(
      environment.productionUrl,
      ProductionControllerNamesEnum.FORMULA
    );

    return ds;
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

  getFormulaVariableTypes(): FormulaVariableTypes[] {
    var varibaleTypes: FormulaVariableTypes[] = [
      { code: 'EN1', name: this._translate.instant('EN1') },
      { code: 'EN2', name: this._translate.instant('EN2') },
      { code: 'EN3', name: this._translate.instant('EN3') },
      { code: 'YUKSEKLIK', name: this._translate.instant('YUKSEKLIK') },
      {
        code: 'SONUCDEGISKENI',
        name: this._translate.instant('SONUCDEGISKENI'),
      },
      { code: 'SABIT', name: this._translate.instant('SABIT') },
      { code: '(', name: '(' },
      { code: ')', name: ')' },
      { code: '+', name: '+' },
      { code: '-', name: '-' },
      { code: '*', name: '*' },
      { code: '/', name: '/' },
    ];

    return varibaleTypes;
  }

  generateFormulaText(formulaDetails: IFormulaDetail[]): string {
    var formulaText: string = '';

    formulaDetails.forEach((item) => {
      formulaText = formulaText + ' ' + item.variableType;
    });

    return formulaText;
  }
}

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
import { FormulaVariableTypeEnums } from 'app/enums/production/formula-variable-type.enum';

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
      {
        code: FormulaVariableTypeEnums.WIDTH1,
        name: this._translate.instant(FormulaVariableTypeEnums.WIDTH1),
      },
      {
        code: FormulaVariableTypeEnums.WIDTH2,
        name: this._translate.instant(FormulaVariableTypeEnums.WIDTH2),
      },
      {
        code: FormulaVariableTypeEnums.WIDTH3,
        name: this._translate.instant(FormulaVariableTypeEnums.WIDTH3),
      },
      {
        code: FormulaVariableTypeEnums.HEIGHT,
        name: this._translate.instant(FormulaVariableTypeEnums.HEIGHT),
      },
      {
        code: FormulaVariableTypeEnums.RESULTVARIABLE,
        name: this._translate.instant(FormulaVariableTypeEnums.RESULTVARIABLE),
      },
      {
        code: FormulaVariableTypeEnums.CONSTANT,
        name: this._translate.instant(FormulaVariableTypeEnums.CONSTANT),
      },
      {
        code: FormulaVariableTypeEnums.OPENPARENTHESIS,
        name: FormulaVariableTypeEnums.OPENPARENTHESIS,
      },
      {
        code: FormulaVariableTypeEnums.CLOSEPARENTHESIS,
        name: FormulaVariableTypeEnums.CLOSEPARENTHESIS,
      },
      {
        code: FormulaVariableTypeEnums.PLUS,
        name: FormulaVariableTypeEnums.PLUS,
      },
      {
        code: FormulaVariableTypeEnums.MINUS,
        name: FormulaVariableTypeEnums.MINUS,
      },
      {
        code: FormulaVariableTypeEnums.MULTIPLY,
        name: FormulaVariableTypeEnums.MULTIPLY,
      },
      {
        code: FormulaVariableTypeEnums.DIVIDE,
        name: FormulaVariableTypeEnums.DIVIDE,
      },
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

  executeFormula(formulaDetails: IFormulaDetail[]): Promise<Object> {
    return this.httpPostRequest('ExecuteFormulaTest', formulaDetails);
  }

  copyFormula(
    formulaId: string,
    formulaCode: string,
    modelId: string
  ): Promise<Object> {
    return this.httpPostRequest('CopyFormula', {
      formulaId: formulaId,
      formulaCode: formulaCode,
      modelId: modelId,
    });
  }
}

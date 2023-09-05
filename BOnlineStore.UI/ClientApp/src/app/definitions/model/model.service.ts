import { DefinitionsControllerNamesEnum } from '../../base-classes/base-enums/definitions-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from '../../../environments/environment';
import { BaseService } from '../../base-classes/base-services/base-service';
import { lastValueFrom, Observable, switchAll } from 'rxjs';
import CustomStore from 'devextreme/data/custom_store';

@Injectable({
  providedIn: 'root',
})
export class ModelService extends BaseService  {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MODEL
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getModelGroupDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.MODELGROUP
    );
  }

  getPanelDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.PANEL
    );
  }

  getRecipeTypeDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl,
      DefinitionsControllerNamesEnum.RECIPETYPE,
      'LoadForCombo'
    );
  }
}

import { DatasourceFunctionsEnum } from './../../base-classes/base-enums/datasource-functions.enum';
import { DefinitionControllerNamesEnum } from './../../base-classes/base-enums/definition-controller-names.enum';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { BaseService } from 'app/main/base-classes/base-services/base-service';
import DataSource from 'devextreme/data/data_source';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ColorService extends BaseService implements Resolve<any> {
  constructor(public _http: HttpClient) {
    super(_http, DefinitionControllerNamesEnum.COLOR);
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }

  getColorGroupDataSource(): DataSource {
    return super.getBaseDataSource(
      DefinitionControllerNamesEnum.COLORGROUP,
      DatasourceFunctionsEnum.GETLOAD,
      DatasourceFunctionsEnum.NOINSERT,
      DatasourceFunctionsEnum.NOUPDATE,
      DatasourceFunctionsEnum.NOREMOVE
    );
  }

  /*getDataSource(): DataSource {
    return new DataSource({
      key: 'id',
      //loadMode: 'raw',
      paginate: true,
      pageSize: 10,
      load: (loadOptions) =>
        this.sendRequest(
          environment.definitionsUrl + 'Color/Load  ',
          'LOADPOST',
          null,
          loadOptions
        ),
      insert: (values: any) =>
        this.sendRequest(
          environment.definitionsUrl + 'Color',
          'INSERT',
          '',
          values
        ),
      update: (key: string, values: any) =>
        this.sendRequest(
          environment.definitionsUrl + 'Color',
          'UPDATE',
          key,
          values
        ),
      remove: (key: string) =>
        this.sendRequest(environment.definitionsUrl + 'Color', 'DELETE', key),
    });
  }

  getColorGroupDataSource(): DataSource {
    return new DataSource({
      key: 'id',
      paginate: true,
      pageSize: 10,
      load: (loadOptions) =>
        this.sendRequest(
          environment.definitionsUrl + 'Color/Load  ',
          'LOADPOST',
          null,
          loadOptions
        ),
    });
  }

  sendRequest(
    url: string,
    method = 'LOAD',
    key: string = '',
    data: any = {}
  ): any {
    let result;

    switch (method) {
      case 'LOAD':
        result = this._http.get(url);
        break;
      case 'LOADPOST':
        result = this._http.post(url, data);
        break;
      case 'INSERT':
        result = this._http.post(url, data);
        break;
      case 'UPDATE':
        result = this._http.put(url + '/' + key, data);
        break;

      case 'DELETE':
        result = this._http.delete(url + '/' + key);
        break;
    }

    return lastValueFrom(result).then((response: any) => response.result);
  }*/
}

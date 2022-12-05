import { DatasourceFunctionsEnum } from './../base-enums/datasource-functions.enum';
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import { environment } from 'environments/environment';
import { lastValueFrom, Observable } from 'rxjs';

export abstract class BaseService {
  constructor(
    public _http: HttpClient,
    @Inject(String) private _controllerName: string
  ) {}

  getBaseDataSource(
    controllerName: string = this._controllerName,
    addLoadFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.NOLOAD,
    addLoadPostFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETLOADPOST,
    addInsertFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETINSERT,
    addUpdateFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETUPDATE,
    addRemoveFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETREMOVE
  ): DataSource {
    var dataSource = new DataSource({
      key: 'id',
      paginate: true,
      pageSize: 10,
      ...(addLoadFunction == DatasourceFunctionsEnum.GETLOAD && {
        load: (loadOptions) =>
          this.sendRequest(
            environment.definitionsUrl + controllerName + '/Load  ',
            'LOAD',
            null,
            loadOptions
          ),
      }),
      ...(addLoadPostFunction == DatasourceFunctionsEnum.GETLOADPOST && {
        load: (loadOptions) =>
          this.sendRequest(
            environment.definitionsUrl + controllerName + '/Load  ',
            'LOADPOST',
            null,
            loadOptions
          ),
      }),
      ...(addInsertFunction == DatasourceFunctionsEnum.GETINSERT && {
        insert: (values: any) =>
          this.sendRequest(
            environment.definitionsUrl + controllerName,
            'INSERT',
            '',
            values
          ),
      }),
      ...(addUpdateFunction == DatasourceFunctionsEnum.GETUPDATE && {
        update: (key: string, values: any) =>
          this.sendRequest(
            environment.definitionsUrl + controllerName,
            'UPDATE',
            key,
            values
          ),
      }),
      ...(addRemoveFunction == DatasourceFunctionsEnum.GETREMOVE && {
        remove: (key: string) =>
          this.sendRequest(
            environment.definitionsUrl + controllerName,
            'DELETE',
            key
          ),
      }),
    });

    return dataSource;

    /*return new DataSource({
      key: 'id',
      paginate: true,
      pageSize: 10,
      load: (loadOptions) =>
        this.sendRequest(
          environment.definitionsUrl + this._controllerName + '/Load  ',
          'LOADPOST',
          null,
          loadOptions
        ),
      insert: (values: any) =>
        this.sendRequest(
          environment.definitionsUrl + this._controllerName,
          'INSERT',
          '',
          values
        ),
      update: (key: string, values: any) =>
        this.sendRequest(
          environment.definitionsUrl + this._controllerName,
          'UPDATE',
          key,
          values
        ),
      remove: (key: string) =>
        this.sendRequest(
          environment.definitionsUrl + this._controllerName,
          'DELETE',
          key
        ),
    });*/
  }

  /*getColorGroupDataSource(): DataSource {
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
  }*/

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

    return lastValueFrom(result).then((response: any) =>
      method === 'LOAD' ? response.result.data : response.result
    );
  }
}

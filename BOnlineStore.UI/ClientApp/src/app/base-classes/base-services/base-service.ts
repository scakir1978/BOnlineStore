import { DatasourceFunctionsEnum } from './../base-enums/datasource-functions.enum';
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import { lastValueFrom } from 'rxjs';
import CustomStore from 'devextreme/data/custom_store';
import { HttpRequestMethodsEnum } from '../base-enums/http-request-methods.enum';

export abstract class BaseService {
  constructor(
    public _http: HttpClient,
    @Inject(String) private _serviceUrl: string,
    @Inject(String) private _controllerName: string
  ) {}

  getBaseDataSource(
    serviceUrl: string = this._serviceUrl,
    controllerName: string = this._controllerName,
    keys: any = 'id',
    addLoadFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.NOLOAD,
    addLoadPostFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETLOADPOST,
    addInsertFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETINSERT,
    addUpdateFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETUPDATE,
    addRemoveFunction: DatasourceFunctionsEnum = DatasourceFunctionsEnum.GETREMOVE
  ): DataSource {
    var dataSource = new DataSource({
      key: keys,
      paginate: true,
      pageSize: 10,
      ...(addLoadFunction == DatasourceFunctionsEnum.GETLOAD && {
        load: (loadOptions: any) =>
          this.sendRequest(
            serviceUrl + controllerName + '/Load  ',
            HttpRequestMethodsEnum.LOAD,
            null!,
            loadOptions
          ),
      }),
      ...(addLoadPostFunction == DatasourceFunctionsEnum.GETLOADPOST && {
        load: (loadOptions: any) =>
          this.sendRequest(
            serviceUrl + controllerName + '/Load  ',
            HttpRequestMethodsEnum.LOADPOST,
            null!,
            loadOptions
          ),
      }),
      ...(addInsertFunction == DatasourceFunctionsEnum.GETINSERT && {
        insert: (values: any) =>
          this.sendRequest(
            serviceUrl + controllerName,
            HttpRequestMethodsEnum.INSERT,
            '',
            values
          ),
      }),
      ...(addUpdateFunction == DatasourceFunctionsEnum.GETUPDATE && {
        update: (key: string, values: any) =>
          this.sendRequest(
            serviceUrl + controllerName,
            HttpRequestMethodsEnum.UPDATE,
            key,
            values
          ),
      }),
      ...(addRemoveFunction == DatasourceFunctionsEnum.GETREMOVE && {
        remove: (key: string) =>
          this.sendRequest(
            serviceUrl + controllerName,
            HttpRequestMethodsEnum.DELETE,
            key
          ),
      }),
    });

    return dataSource;
  }

  //Comboboxlar için
  getBaseRawCustomStore(
    serviceUrl: string = this._serviceUrl,
    controllerName: string = this._controllerName,
    functionName: string = 'Load',
    keys: any = 'id'
  ): CustomStore {
    return new CustomStore({
      key: keys,
      loadMode: 'raw',
      load: (loadOptions) =>
        this.sendRequest(
          serviceUrl + controllerName + '/' + functionName,
          HttpRequestMethodsEnum.LOAD,
          null!,
          loadOptions
        ),
    });
  }

  sendRequest(
    url: string,
    method: HttpRequestMethodsEnum = HttpRequestMethodsEnum.LOADPOST,
    key: string = '',
    data: any = {}
  ): any {
    let result;

    switch (method) {
      case HttpRequestMethodsEnum.LOAD: //Comboboxlar için
        result = this._http.post(url, data);
        break;
      case HttpRequestMethodsEnum.LOADPOST:
        result = this._http.post(url, data);
        break;
      case HttpRequestMethodsEnum.GETALL:
        result = this._http.get(url);
        break;
      case HttpRequestMethodsEnum.GETBYID:
        result = this._http.get(url + '/' + key);
        break;
      case HttpRequestMethodsEnum.INSERT:
        result = this._http.post(url, data);
        break;
      case HttpRequestMethodsEnum.UPDATE:
        result = this._http.put(url + '/' + key, data);
        break;
      case HttpRequestMethodsEnum.DELETE:
        result = this._http.delete(url + '/' + key);
        break;
    }

    return lastValueFrom(result).then((response: any) =>
      method === 'LOAD' ? response.result.data : response.result
    );
  }
}

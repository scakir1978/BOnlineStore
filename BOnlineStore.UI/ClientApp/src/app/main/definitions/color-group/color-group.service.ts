import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from 'environments/environment';
import { lastValueFrom, Observable, switchAll } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ColorGroupService implements Resolve<any> {
  constructor(private _http: HttpClient) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return new DataSource({
      key: 'id',
      //loadMode: 'raw',
      paginate: true,
      pageSize: 10,
      load: (loadOptions) =>
        this.sendRequest(
          environment.definitionsUrl + 'ColorGroups/Load  ',
          'LOADPOST',
          null,
          loadOptions
        ),
      insert: (values: any) =>
        this.sendRequest(
          environment.definitionsUrl + 'ColorGroups',
          'INSERT',
          '',
          values
        ),
      update: (key: string, values: any) =>
        this.sendRequest(
          environment.definitionsUrl + 'ColorGroups',
          'UPDATE',
          key,
          values
        ),
      remove: (key: string) =>
        this.sendRequest(
          environment.definitionsUrl + 'ColorGroups',
          'DELETE',
          key
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
    /*.catch((err) => {
        Swal.fire({
          icon: 'error',
          title: 'Hata oluştu..',
          text: err,
          customClass: {
            confirmButton: 'btn btn-primary',
          },
        });
      });*/
  }
}

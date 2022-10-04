
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import DataSource from 'devextreme/data/data_source';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class ModelGroupService implements Resolve<any> {

  constructor(private _http : HttpClient) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    
  }
  
  getDataSource(): DataSource {
    return new DataSource({
      key: "id",
      loadMode: "raw",
      paginate: true,
      pageSize: 10,
      load: () => this._http.get(environment.definitionsUrl + "modelgroups").toPromise().then((response : any)=>{
        return response.data;
      })
    })
  }
}

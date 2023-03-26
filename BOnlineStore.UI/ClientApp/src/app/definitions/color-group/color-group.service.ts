import { DefinitionControllerNamesEnum } from "./../../base-classes/base-enums/definition-controller-names.enum";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from "@angular/router";
import DataSource from "devextreme/data/data_source";
import { environment } from "../../../environments/environment";
import { BaseService } from "../../base-classes/base-services/base-service";
import { lastValueFrom, Observable, switchAll } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ColorGroupService extends BaseService implements Resolve<any> {
  constructor(public override _http: HttpClient) {
    super(
      _http,
      environment.definitionsUrl,
      DefinitionControllerNamesEnum.COLORGROUP
    );
  }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {}

  getDataSource(): DataSource {
    return super.getBaseDataSource();
  }
}

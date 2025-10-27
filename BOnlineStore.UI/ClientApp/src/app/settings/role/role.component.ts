import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { RoleService } from "./role.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "role",
  templateUrl: "./role.component.html",
  styleUrls: ["./role.component.scss"],
})
export class RoleComponent extends BaseDefinitionsOnGridComponent {
  public roleDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _roleService: RoleService
  ) {
    super(
      _translate,
      "ROLE", //Pdf, excel dosya adı
      "ROLE", //breadCrump için kullanılacak componenet keyi
      "SETTINGS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.roleDataSource = _roleService.getDataSource();
  }
}

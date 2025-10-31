import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { UserService } from "./user.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "user",
  templateUrl: "./user.component.html",
  styleUrls: ["./user.component.scss"],
})
export class UserComponent extends BaseDefinitionsOnGridComponent {
  public userDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _userService: UserService
  ) {
    super(
      _translate,
      "USER", //Pdf, excel dosya adı
      "USER", //breadCrump için kullanılacak componenet keyi
      "SETTINGS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.userDataSource = _userService.getDataSource();
  }
}

import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { FirmTypeService } from "./firm-type.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "firm-type",
  templateUrl: "./firm-type.component.html",
  styleUrls: ["./firm-type.component.scss"],
})
export class FirmTypeComponent extends BaseDefinitionsOnGridComponent {
  public firmTypeDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _firmTypeService: FirmTypeService
  ) {
    super(
      _translate,
      "FIRMTYPE", //Pdf, excel dosya adı
      "FIRMTYPE", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.firmTypeDataSource = _firmTypeService.getDataSource();
  }
}

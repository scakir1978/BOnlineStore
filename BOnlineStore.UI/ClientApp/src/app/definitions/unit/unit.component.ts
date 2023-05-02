import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { UnitService } from "./unit.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "unit",
  templateUrl: "./unit.component.html",
  styleUrls: ["./unit.component.scss"],
})
export class UnitComponent extends BaseDefinitionsOnGridComponent {
  public unitDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _unitService: UnitService
  ) {
    super(
      _translate,
      "UNIT", //Pdf, excel dosya adı
      "UNIT", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.unitDataSource = _unitService.getDataSource();
  }
}

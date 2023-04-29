import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { RawMaterialGroupService } from "./raw-material-group.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "raw-material-group",
  templateUrl: "./raw-material-group.component.html",
  styleUrls: ["./raw-material-group.component.scss"],
})
export class RawMaterialGroupComponent extends BaseDefinitionsOnGridComponent {
  public rawMaterialGroupDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _rawMaterialGroupService: RawMaterialGroupService
  ) {
    super(
      _translate,
      "RAWMATERIALGROUP", //Pdf, excel dosya adı
      "RAWMATERIALGROUP", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.rawMaterialGroupDataSource = _rawMaterialGroupService.getDataSource();
  }
}

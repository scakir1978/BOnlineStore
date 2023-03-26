import { BaseDefinitionsOnGridComponent } from "./../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { ModelGroupService } from "./model-group.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "model-group",
  templateUrl: "./model-group.component.html",
  styleUrls: ["./model-group.component.scss"],
})
export class ModelGroupComponent extends BaseDefinitionsOnGridComponent {
  public modelGroupDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _modelGroupService: ModelGroupService
  ) {
    super(
      _translate,
      "MODELGROUPS", //Pdf, excel dosya adı
      "MODELGROUPS", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.modelGroupDataSource = _modelGroupService.getDataSource();
  }
}

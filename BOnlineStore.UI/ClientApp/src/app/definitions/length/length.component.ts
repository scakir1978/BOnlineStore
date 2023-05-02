import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { LengthService } from "./length.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "length",
  templateUrl: "./length.component.html",
  styleUrls: ["./length.component.scss"],
})
export class LengthComponent extends BaseDefinitionsOnGridComponent {
  public lengthDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _lengthService: LengthService
  ) {
    super(
      _translate,
      "LENGTH", //Pdf, excel dosya adı
      "LENGTH", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.lengthDataSource = _lengthService.getDataSource();
  }
}

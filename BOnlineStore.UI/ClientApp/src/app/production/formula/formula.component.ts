import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { FormulaService } from "./formula.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "formula",
  templateUrl: "./formula.component.html",
  styleUrls: ["./formula.component.scss"],
})
export class FormulaComponent extends BaseDefinitionsOnGridComponent {
  public formulaDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _formulaService: FormulaService
  ) {
    super(
      _translate,
      "FORMULA", //Pdf, excel dosya adı
      "FORMULA", //breadCrump için kullanılacak componenet keyi
      "PRODUCTION" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.formulaDataSource = _formulaService.getDataSource();
  }
}

import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { BankService } from "./bank.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "bank",
  templateUrl: "./bank.component.html",
  styleUrls: ["./bank.component.scss"],
})
export class BankComponent extends BaseDefinitionsOnGridComponent {
  public bankDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _bankService: BankService
  ) {
    super(
      _translate,
      "BANK", //Pdf, excel dosya adı
      "BANK", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.bankDataSource = _bankService.getDataSource();
  }
}

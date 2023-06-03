import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { CountryService } from "./country.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "country",
  templateUrl: "./country.component.html",
  styleUrls: ["./country.component.scss"],
})
export class CountryComponent extends BaseDefinitionsOnGridComponent {
  public countryDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _countryService: CountryService
  ) {
    super(
      _translate,
      "COUNTRY", //Pdf, excel dosya adı
      "COUNTRY", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.countryDataSource = _countryService.getDataSource();
  }
}

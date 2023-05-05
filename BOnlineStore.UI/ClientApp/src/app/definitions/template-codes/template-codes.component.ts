import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { TemplateCodesService } from "./template-codes.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "template-codes",
  templateUrl: "./template-codes.component.html",
  styleUrls: ["./template-codes.component.scss"],
})
export class TemplateCodesComponent extends BaseDefinitionsOnGridComponent {
  public templateCodesDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _templateCodesService: TemplateCodesService
  ) {
    super(
      _translate,
      "TEMPLATECODES", //Pdf, excel dosya adı
      "TEMPLATECODES", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.templateCodesDataSource = _templateCodesService.getDataSource();
  }
}

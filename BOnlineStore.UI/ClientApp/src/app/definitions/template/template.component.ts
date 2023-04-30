import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { TemplateService } from "./template.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "template",
  templateUrl: "./template.component.html",
  styleUrls: ["./template.component.scss"],
})
export class TemplateComponent extends BaseDefinitionsOnGridComponent {
  public templateDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _templateService: TemplateService
  ) {
    super(
      _translate,
      "TEMPLATE", //Pdf, excel dosya adı
      "TEMPLATE", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.templateDataSource = _templateService.getDataSource();
  }
}

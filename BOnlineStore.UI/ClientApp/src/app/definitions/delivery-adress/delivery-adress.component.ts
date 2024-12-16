import { BaseDefinitionsOnGridComponent } from "../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component";
import { DeliveryAdressService } from "./delivery-adress.service";
import { Component } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: "delivery-adress",
  templateUrl: "./delivery-adress.component.html",
  styleUrls: ["./delivery-adress.component.scss"],
})
export class DeliveryAdressComponent extends BaseDefinitionsOnGridComponent {
  public deliveryAdressDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _deliveryAdressService: DeliveryAdressService
  ) {
    super(
      _translate,
      "DELIVERYADRESS", //Pdf, excel dosya adı
      "DELIVERYADRESS", //breadCrump için kullanılacak componenet keyi
      "DEFINITIONS" //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.deliveryAdressDataSource = _deliveryAdressService.getDataSource();
  }
}

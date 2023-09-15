import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { PriceListService } from './price-list.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';
import { PriceListMaster } from './models/price-list-form-model';
import { ObjectId } from 'bson';

@Component({
  selector: 'price-list',
  templateUrl: './price-list.component.html',
  styleUrls: ['./price-list.component.scss'],
})
export class PriceListComponent extends BaseDefinitionsOnGridComponent {
  public priceListDataSource: DataSource;
  public priceListMaster: PriceListMaster;
  public formActive: boolean = false;

  constructor(
    public override _translate: TranslateService,
    private _priceListService: PriceListService
  ) {
    super(
      _translate,
      'PRICELIST', //Pdf, excel dosya adı
      'PRICELIST', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.formActive = false;
    this.priceListDataSource = _priceListService.getDataSource();
  }

  newPriceList(e) {
    var objectId = new ObjectId();
    this.formActive = true;
    this.priceListMaster = new PriceListMaster(objectId.toString());
    this.priceListMaster.code = '123';
  }

  editPriceList(e) {}

  removePriceList(e) {}

  submitForm(priceListMaster: PriceListMaster) {
    this.formActive = false;
  }
}

import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { PriceListService } from './price-list.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';
import { PriceListMaster } from './models/price-list-form-model';
import { ObjectId } from 'bson';
import { FormCrudTypeEnum } from 'app/base-classes/base-enums/form-crud-type.enum';
import Swal from 'sweetalert2';

@Component({
  selector: 'price-list',
  templateUrl: './price-list.component.html',
  styleUrls: ['./price-list.component.scss'],
})
export class PriceListComponent extends BaseDefinitionsOnGridComponent {
  public priceListDataSource: DataSource;
  public priceListMaster: PriceListMaster;
  public formActive: boolean = false;
  public formCrudType: FormCrudTypeEnum;

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

    this.removePriceList = this.removePriceList.bind(this);
    this.editPriceList = this.editPriceList.bind(this);
    this.submitForm = this.submitForm.bind(this);
  }

  newPriceList(e) {
    var objectId = new ObjectId();
    this.formActive = true;
    this.priceListMaster = new PriceListMaster(objectId.toString());
    this.formCrudType = FormCrudTypeEnum.INSERT;
  }

  async editPriceList(e) {
    this.formCrudType = FormCrudTypeEnum.UPDATE;
    this.priceListMaster = await this._priceListService.getById(e.row.data.id);
    this.formActive = true;
    e.event.preventDefault();
  }

  removePriceList(e) {
    this._priceListService.delete(e.row.data.id);
    this.refreshDataGrid();
    e.event.preventDefault();
  }

  async submitForm(priceListMaster: PriceListMaster) {
    if (this.formCrudType == FormCrudTypeEnum.INSERT) {
      try {
        await this._priceListService.insert(priceListMaster);
      } catch (error) {
        console.log(error);
        return;
      }
    }
    if (this.formCrudType == FormCrudTypeEnum.UPDATE) {
      try {
        await this._priceListService.update(
          priceListMaster,
          priceListMaster.id
        );
      } catch (error) {
        this.showErrorMessage(error.message);
        return;
      }
    }

    this.formActive = false;
    this.refreshDataGrid();
  }

  cancelForm() {
    this.formActive = false;
  }

  showErrorMessage(errorMessage) {
    Swal.fire({
      title: 'Değişiklikler kaydedilemedi.',
      text: errorMessage,
      icon: 'error',
      confirmButtonColor: '#364574',
      confirmButtonText: 'OK',
    });
  }
}

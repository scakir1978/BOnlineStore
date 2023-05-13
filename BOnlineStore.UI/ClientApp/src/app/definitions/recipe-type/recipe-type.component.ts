import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { RecipeTypeService } from './recipe-type.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'recipe-type',
  templateUrl: './recipe-type.component.html',
  styleUrls: ['./recipe-type.component.scss'],
})
export class RecipeTypeComponent extends BaseDefinitionsOnGridComponent {
  public recipeTypeDataSource: DataSource;
  public rawMaterialDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _recipeTypeService: RecipeTypeService
  ) {
    super(
      _translate,
      'RECIPETYPE', //Pdf, excel dosya adı
      'RECIPETYPE', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.recipeTypeDataSource = _recipeTypeService.getDataSource();
    this.rawMaterialDataSource = _recipeTypeService.getRawMaterialDataSource();
  }

  onInitNewRow(e: any) {
    e.data.rawMaterialIds = [];
    e.data.panelRawMaterialIds = [];
    e.data.glassLengthRawMaterialIds = [];
    e.data.glassWidthRawMaterialIds = [];
    e.data.panelGlassLengthRawMaterialIds = [];
    e.data.panelGlassWidthRawMaterialIds = [];
  }

  onSaved(e: any, data: any) {
    var values = e.component.getDataSource().items();
    data.setValue(values);
  }

  onRowInserting(e) {
    var values: any[] = [];
    values = e.component.getDataSource().items();

    const found = values.find((data) => {
      return data.id === e.data.id;
    });

    if (found) {
      e.cancel = true;
      e.component
        .instance()
        .getController('editing')
        ._fireDataErrorOccurred(this._translate.instant('DUPLICATEKEY'));
    }
  }
}

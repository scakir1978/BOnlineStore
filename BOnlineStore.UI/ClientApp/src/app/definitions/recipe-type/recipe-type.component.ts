import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { RecipeTypeService } from './recipe-type.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

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
  }

  onSaved(e: any, data: any) {
    var values = e.component.getDataSource().items();
    data.setValue(values);
  }
}

import { FormName } from '../../enums/production/form-name.enum';
import { ICodeName } from './../../base-classes/base-interfaces/code-name-interface';
import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { RecipeTypeService } from './recipe-type.service';
import { Component } from '@angular/core';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
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

  public formNameList: ICodeName[];

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
    this.translateAndGetArrays();
    this._translate.onLangChange.subscribe((value: LangChangeEvent) => {
      this.translateAndGetArrays();
    });
  }

  onInitNewRow(e: any) {
    e.data.rawMaterialIds = [];
    e.data.panelRawMaterialIds = [];
    e.data.glassRawMaterialIds = [];
    e.data.panelGlassRawMaterialIds = [];
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

  translateAndGetArrays() {
    this._translate
      .get(['DEFAULTFORM', 'FORMNO1', 'FORMNO2', 'FORMNO3', 'FORMNO4'])
      .subscribe((translations) => {
        this.formNameList = [
          {
            code: FormName.DefaultForm,
            name: translations['DEFAULTFORM'],
          },
          {
            code: FormName.FormNo1,
            name: translations['FORMNO1'],
          },
          {
            code: FormName.FormNo2,
            name: translations['FORMNO2'],
          },
          {
            code: FormName.FormNo3,
            name: translations['FORMNO3'],
          },
          {
            code: FormName.FormNo4,
            name: translations['FORMNO4'],
          },
        ];
      });
  }

  onRawMaterialGridSelectionChanged(
    selectedRowKeys,
    cellInfo,
    dropDownBoxComponent
  ) {
    cellInfo.setValue(selectedRowKeys[0]);
    if (selectedRowKeys.length > 0) {
      dropDownBoxComponent.close();
    }
  }
}

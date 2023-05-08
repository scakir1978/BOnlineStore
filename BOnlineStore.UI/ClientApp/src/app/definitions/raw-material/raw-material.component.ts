import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { RawMaterialService } from './raw-material.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'raw-material',
  templateUrl: './raw-material.component.html',
  styleUrls: ['./raw-material.component.scss'],
})
export class RawMaterialComponent extends BaseDefinitionsOnGridComponent {
  public rawMaterialDataSource: DataSource;
  public rawMaterialGroupDataSource: CustomStore;
  public rawMaterialMasterGroupDataSource: CustomStore;
  public unitDataSource: CustomStore;
  stokSubDetailItems: any[] = [];

  constructor(
    public override _translate: TranslateService,
    private _rawMaterialService: RawMaterialService
  ) {
    super(
      _translate,
      'RAWMATERIAL', //Pdf, excel dosya adı
      'RAWMATERIAL', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );

    _translate.onLangChange.subscribe((lang) => {
      _translate
        .get(['NONE', 'COLORED', 'GLASSPATTERN', 'LENGTH'])
        .subscribe((translations) => {
          this.stokSubDetailItems = [
            { id: 0, text: translations.NONE },
            { id: 1, text: translations.COLORED },
            { id: 2, text: translations.GLASSPATTERN },
            { id: 3, text: translations.LENGTH },
          ];
        });
    });

    this.rawMaterialDataSource = _rawMaterialService.getDataSource();
    this.unitDataSource = _rawMaterialService.getUnitDataSource();
  }
}

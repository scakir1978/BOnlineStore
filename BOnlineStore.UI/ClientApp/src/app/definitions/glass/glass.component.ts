import CustomStore from 'devextreme/data/custom_store';
import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { GlassService } from './glass.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'glass',
  templateUrl: './glass.component.html',
  styleUrls: ['./glass.component.scss'],
})
export class GlassComponent extends BaseDefinitionsOnGridComponent {
  public glassDataSource: DataSource;
  public glassGroupDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _glassService: GlassService
  ) {
    super(
      _translate,
      'GLASS', //Pdf, excel dosya adı
      'GLASS', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );

    this.glassGroupDataSource = _glassService.getGlassGroupDataSource();
    this.glassDataSource = _glassService.getDataSource();
  }
}

import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { GlassGroupService } from './glass-group.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'glass-group',
  templateUrl: './glass-group.component.html',
  styleUrls: ['./glass-group.component.scss'],
})
export class GlassGroupComponent extends BaseDefinitionsOnGridComponent {
  public glassGroupDataSource: DataSource;

  constructor(
    public override _translate: TranslateService,
    private _glassGroupService: GlassGroupService
  ) {
    super(
      _translate,
      'GLASSGROUP', //Pdf, excel dosya adı
      'GLASSGROUP', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.glassGroupDataSource = _glassGroupService.getDataSource();
  }
}

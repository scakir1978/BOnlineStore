import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { AssemblyPriceService } from './assembly-price.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'assembly-price',
  templateUrl: './assembly-price.component.html',
  styleUrls: ['./assembly-price.component.scss'],
})
export class AssemblyPriceComponent extends BaseDefinitionsOnGridComponent {
  public assemblyPriceDataSource: DataSource;
  public regionDataSource: CustomStore;
  public glassDataSource: CustomStore;

  constructor(
    public override _translate: TranslateService,
    private _assemblyPriceService: AssemblyPriceService
  ) {
    super(
      _translate,
      'ASSEMBLYPRICE', //Pdf, excel dosya adı
      'ASSEMBLYPRICE', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.assemblyPriceDataSource = _assemblyPriceService.getDataSource();
    this.regionDataSource = _assemblyPriceService.getRegionDataSource();
    this.glassDataSource = _assemblyPriceService.getGlassDataSource();
  }
}

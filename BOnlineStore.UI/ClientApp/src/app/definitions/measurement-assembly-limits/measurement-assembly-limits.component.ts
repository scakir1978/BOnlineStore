import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import {
  Day,
  MeasurementAssemblyLimitsService,
} from './measurement-assembly-limits.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'measurement-assembly-limits',
  templateUrl: './measurement-assembly-limits.component.html',
  styleUrls: ['./measurement-assembly-limits.component.scss'],
})
export class MeasurementAssemblyLimitsComponent extends BaseDefinitionsOnGridComponent {
  public measurementAssemblyLimitsDataSource: DataSource;
  public assemblerDataSource: CustomStore;
  public regionDataSource: CustomStore;
  public daysDataSource: any[] = [];

  constructor(
    public override _translate: TranslateService,
    private _measurementAssemblyLimitsService: MeasurementAssemblyLimitsService
  ) {
    super(
      _translate,
      'MEASUREMENTASSEMBLYLIMITS', //Pdf, excel dosya adı
      'MEASUREMENTASSEMBLYLIMITS', //breadCrump için kullanılacak componenet keyi
      'DEFINITIONS' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );

    this.measurementAssemblyLimitsDataSource =
      _measurementAssemblyLimitsService.getDataSource();
    this.assemblerDataSource =
      _measurementAssemblyLimitsService.getAssemblerDataSource();
    this.regionDataSource =
      _measurementAssemblyLimitsService.getRegionDataSource();

    _measurementAssemblyLimitsService
      .getDaysDataSource()
      .subscribe((translations) => {
        this.daysDataSource = [
          { id: 1, name: translations['MONDAY'] },
          { id: 2, name: translations['TUESDAY'] },
          { id: 3, name: translations['WEDNESDAY'] },
          { id: 4, name: translations['THURSDAY'] },
          { id: 5, name: translations['FRIDAY'] },
          { id: 6, name: translations['SATURDAY'] },
          { id: 7, name: translations['SUNDAY'] },
        ];
      });

    this._translate.onLangChange.subscribe((lang) => {
      _measurementAssemblyLimitsService
        .getDaysDataSource()
        .subscribe((translations) => {
          this.daysDataSource = [
            { id: 1, name: translations['MONDAY'] },
            { id: 2, name: translations['TUESDAY'] },
            { id: 3, name: translations['WEDNESDAY'] },
            { id: 4, name: translations['THURSDAY'] },
            { id: 5, name: translations['FRIDAY'] },
            { id: 6, name: translations['SATURDAY'] },
            { id: 7, name: translations['SUNDAY'] },
          ];
        });
    });
  }

  atLeastOneRegionEntered(e) {
    if (
      !e.data.region01Id &&
      !e.data.region02Id &&
      !e.data.region03Id &&
      !e.data.region04Id &&
      !e.data.region05Id
    ) {
      return false;
    }

    return true;
  }
  atLeastOneQuantityEntered(e) {
    if (!e.data.measurementQuantity && !e.data.assemblyQuantity) {
      return false;
    }
    return true;
  }
}

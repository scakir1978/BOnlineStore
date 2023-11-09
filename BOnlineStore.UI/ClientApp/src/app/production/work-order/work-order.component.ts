import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { WorkOrderService } from './work-order.service';
import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ICodeName } from 'app/base-classes/base-interfaces/code-name-interface';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

@Component({
  selector: 'work-order',
  templateUrl: './work-order.component.html',
  styleUrls: ['./work-order.component.scss'],
})
export class WorkOrderComponent extends BaseDefinitionsOnGridComponent {
  public workOrderDataSource: DataSource;
  public modelDataSource: CustomStore;
  public colorDataSource: CustomStore;
  public glassDataSource: CustomStore;
  public firmDataSource: CustomStore;
  public templateDataSource: CustomStore;
  public swingDirectionList: ICodeName[];
  public workOrderStatusList: ICodeName[];

  constructor(
    public override _translate: TranslateService,
    private _workOrderService: WorkOrderService
  ) {
    super(
      _translate,
      'WORKORDER', //Pdf, excel dosya adı
      'WORKORDER', //breadCrump için kullanılacak componenet keyi
      'PRODUCTION' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.workOrderDataSource = _workOrderService.getDataSource();
    this.modelDataSource = _workOrderService.getRawModelDataSource();
    this.colorDataSource = _workOrderService.getRawColorDataSource();
    this.glassDataSource = _workOrderService.getRawGlassDataSource();
    this.firmDataSource = _workOrderService.getRawFirmDataSource();
    this.templateDataSource = _workOrderService.getRawTemplateDataSource();
    this.swingDirectionList = _workOrderService.getSwingDirectionList();
    this.workOrderStatusList = _workOrderService.getWorkOrderStatusList();
  }
}

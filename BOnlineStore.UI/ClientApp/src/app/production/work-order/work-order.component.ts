import { WorkOrderFormFrontEndDto } from './../../dtos/production/work-order-form-front-end-interface';
import { ColorGroupComponent } from './../../definitions/color-group/color-group.component';
import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { WorkOrderService } from './work-order.service';
import { Component } from '@angular/core';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import { ICodeName } from 'app/base-classes/base-interfaces/code-name-interface';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { WorkOrderStatusEnum } from './enums/work-order-status.enum';
import { SwingDirectionEnum } from './enums/swing-direction.enum';

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
  public workOrderFormFrontEndDto: WorkOrderFormFrontEndDto;

  dropDownOptions: object;

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

    this.translateAndGetArrays();
    this._translate.onLangChange.subscribe((value: LangChangeEvent) => {
      this.translateAndGetArrays();
    });

    this.showWorkOrderForm = this.showWorkOrderForm.bind(this);
    this.dropDownOptions = { width: 500 };
  }

  translateAndGetArrays() {
    this._translate
      .get([
        'ASSEMBLY',
        'CARGO',
        'DEALERDELIVERY',
        'FACTORYFINISHED',
        'LEFTHANDED',
        'NONE',
        'RIGHTHANDED',
      ])
      .subscribe((translations) => {
        this.workOrderStatusList = [
          {
            code: WorkOrderStatusEnum.ASSEMBLY,
            name: translations['ASSEMBLY'],
          },
          {
            code: WorkOrderStatusEnum.CARGO,
            name: translations['CARGO'],
          },
          {
            code: WorkOrderStatusEnum.DEALERDELIVERY,
            name: translations['DEALERDELIVERY'],
          },
          {
            code: WorkOrderStatusEnum.FACTORYFINISHED,
            name: translations['FACTORYFINISHED'],
          },
        ];

        this.swingDirectionList = [
          {
            code: SwingDirectionEnum.LEFTHANDED,
            name: translations['LEFTHANDED'],
          },
          {
            code: SwingDirectionEnum.NONE,
            name: translations['NONE'],
          },
          {
            code: SwingDirectionEnum.RIGHTHANDED,
            name: translations['RIGHTHANDED'],
          },
        ];
      });
  }

  async showWorkOrderForm(e) {
    this._workOrderService
      .calculateProductionListBff(e.row.data.id)
      .then((result: WorkOrderFormFrontEndDto) => {
        this.workOrderFormFrontEndDto = result;
      });
  }

  onModelGridSelectionChanged(selectedRowKeys, cellInfo, dropDownBoxComponent) {
    cellInfo.setValue(selectedRowKeys[0]);
    if (selectedRowKeys.length > 0) {
      dropDownBoxComponent.close();
    }
  }
}

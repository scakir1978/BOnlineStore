import { AlertService } from './../../core/services/alert.service';
import { FormName } from './../../enums/production/form-name.enum';
import { WorkOrderFormFrontEndDto } from './../../dtos/production/work-order-form-front-end-interface';
import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { WorkOrderService } from './work-order.service';
import { Component } from '@angular/core';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import { ICodeName } from 'app/base-classes/base-interfaces/code-name-interface';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { WorkOrderStatus } from '../../enums/production/work-order-status.enum';
import { SwingDirection } from '../../enums/production/swing-direction.enum';
import Swal from 'sweetalert2';

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
  public workOrderFormDto: WorkOrderFormFrontEndDto;
  public workOrderDefaultFormName: FormName = FormName.DefaultForm;
  public workOrderFormName: FormName = FormName.DefaultForm;
  public formActive: boolean = false;
  public loadingVisible = false;

  dropDownOptions: object;

  constructor(
    public override _translate: TranslateService,
    private _workOrderService: WorkOrderService,
    private _alertService: AlertService
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

    this.formActive = false;
    this.workOrderFormName = FormName.DefaultForm;
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
            code: WorkOrderStatus.Assembly,
            name: translations['ASSEMBLY'],
          },
          {
            code: WorkOrderStatus.Cargo,
            name: translations['CARGO'],
          },
          {
            code: WorkOrderStatus.DealerDelivery,
            name: translations['DEALERDELIVERY'],
          },
          {
            code: WorkOrderStatus.FactoryFinished,
            name: translations['FACTORYFINISHED'],
          },
        ];

        this.swingDirectionList = [
          {
            code: SwingDirection.LeftHanded,
            name: translations['LEFTHANDED'],
          },
          {
            code: SwingDirection.None,
            name: translations['NONE'],
          },
          {
            code: SwingDirection.RightHanded,
            name: translations['RIGHTHANDED'],
          },
        ];
      });
  }

  async showWorkOrderForm(e) {
    this.loadingVisible = true;

    this._workOrderService
      .calculateProductionListBff(e.row.data.id)
      .then((result: WorkOrderFormFrontEndDto) => {
        this.workOrderFormDto = result;
        this.workOrderFormName = this.workOrderFormDto.recipeType.formName;
        this.loadingVisible = false;
        this.formActive = true;
      })
      .catch((reason) => {
        this.loadingVisible = false;
        this._alertService.showErrorMessage(reason.message);
      });
  }

  onModelGridSelectionChanged(selectedRowKeys, cellInfo, dropDownBoxComponent) {
    cellInfo.setValue(selectedRowKeys[0]);
    if (selectedRowKeys.length > 0) {
      dropDownBoxComponent.close();
    }
  }

  closeForm() {
    this.formActive = false;
  }
}

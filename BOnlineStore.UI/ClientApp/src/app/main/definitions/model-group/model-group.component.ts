import { ModelGroupService } from './model-group.service';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { CoreConfig } from '@core/types';
import { TranslateService } from '@ngx-translate/core';
import DataSource from 'devextreme/data/data_source';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Workbook } from 'exceljs';
import { saveAs } from 'file-saver';
import { exportDataGrid as exportDataGridExcel } from 'devextreme/excel_exporter';

import { exportDataGrid as exportDataGridPdf } from 'devextreme/pdf_exporter';
import { jsPDF } from 'jspdf';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'model-group',
  templateUrl: './model-group.component.html',
  styleUrls: ['./model-group.component.scss'],
})
export class ModelGroupComponent implements OnInit, OnDestroy {
  @ViewChild(DxDataGridComponent, { static: false })
  dataGrid: DxDataGridComponent;

  // public
  public contentHeader: object;

  public modelGroupDataSource: DataSource;

  private _unsubscribeAll: Subject<any>;

  constructor(
    private _translate: TranslateService,
    private _coreConfigService: CoreConfigService,
    private _modelGroupService: ModelGroupService
  ) {
    this.modelGroupDataSource = _modelGroupService.getDataSource();
    this._unsubscribeAll = new Subject();
  }

  // Lifecycle Hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
  ngOnInit() {
    this.createBreadCrumb();

    this._coreConfigService
      .getConfig()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((response: CoreConfig) => {
        this.createBreadCrumb();
      });
  }

  onExporting(e) {
    e.fileName = 'ModelGruplari';
    if (e.format === 'xslx') {
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('ModelGruplari');

      exportDataGridExcel({
        component: e.component,
        worksheet,
        autoFilterEnabled: true,
      }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {
          saveAs(
            new Blob([buffer], { type: 'application/octet-stream' }),
            'ModelGruplari.xlsx'
          );
        });
      });
      e.cancel = true;
    }
    if (e.format === 'pdf') {
      const doc = new jsPDF();
      exportDataGridPdf({
        jsPDFDocument: doc,
        component: e.component,
        indent: 5,
      }).then(() => {
        doc.save('ModelGruplari.pdf');
      });
    }
  }

  private createBreadCrumb() {
    this.contentHeader = {
      headerTitle: this._translate.instant('KEYS.MODELGROUPS'),
      actionButton: true,
      breadcrumb: {
        type: '',
        links: [
          {
            name: this._translate.instant('KEYS.DEFINITIONS'),
            isLink: false,
          },
          {
            name: this._translate.instant('KEYS.MODELGROUPS'),
            isLink: false,
          },
        ],
      },
    };
  }

  refreshDataGrid() {
    this.dataGrid.instance.refresh();
  }

  /**
   * On destroy
   */
  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(true);
    this._unsubscribeAll.complete();
  }
}

import { Component, OnInit, OnDestroy, ViewChild, Inject } from '@angular/core';
import { CoreConfigService } from '@core/services/config.service';
import { CoreConfig } from '@core/types';
import { TranslateService } from '@ngx-translate/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Workbook } from 'exceljs';
import { saveAs } from 'file-saver';
import { exportDataGrid as exportDataGridExcel } from 'devextreme/excel_exporter';

import { exportDataGrid as exportDataGridPdf } from 'devextreme/pdf_exporter';
import { jsPDF } from 'jspdf';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'app-base-definitions-on-grid',
  template: `<p>base works!</p>`,
  styles: [],
})
export class BaseDefinitionsOnGridComponent implements OnInit, OnDestroy {
  @ViewChild(DxDataGridComponent, { static: false })
  dataGrid: DxDataGridComponent;

  // public
  public contentHeader: object;

  private _unsubscribeAll: Subject<any>;

  private fileName: string;
  private parentKey: string;
  private componentKey: string;

  constructor(
    public _translate: TranslateService,
    public _coreConfigService: CoreConfigService,
    @Inject(String) _fileNameKey: string,
    @Inject(String) _componentKey: string,
    @Inject(String) _parentKey: string
  ) {
    this.fileName = this._translate.instant(_fileNameKey);
    this.componentKey = _componentKey;
    this.parentKey = _parentKey;
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
    e.fileName = this.fileName;
    if (e.format === 'xslx') {
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet(this.fileName);

      exportDataGridExcel({
        component: e.component,
        worksheet,
        autoFilterEnabled: true,
      }).then(() => {
        workbook.xlsx.writeBuffer().then((buffer) => {
          saveAs(
            new Blob([buffer], { type: 'application/octet-stream' }),
            this.fileName + '.xlsx'
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
        doc.save(this.fileName + '.pdf');
      });
    }
  }

  private createBreadCrumb() {
    this.contentHeader = {
      headerTitle: this._translate.instant(this.componentKey),
      actionButton: true,
      breadcrumb: {
        type: '',
        links: [
          {
            name: this._translate.instant(this.parentKey),
            isLink: false,
          },
          {
            name: this._translate.instant(this.componentKey),
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

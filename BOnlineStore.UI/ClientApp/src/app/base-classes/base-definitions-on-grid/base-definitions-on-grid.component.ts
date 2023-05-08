import { Component, OnInit, OnDestroy, ViewChild, Inject } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subject } from 'rxjs';

import * as ExcelJS from 'exceljs';
import { EventEmitter } from '@angular/core';
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
  dataGrid!: DxDataGridComponent;

  // public

  private _unsubscribeAll: Subject<any>;

  private fileName: string;
  private parentKey: string;
  private componentKey: string;

  // bread crumb items
  breadCrumbItems!: Array<{}>;

  constructor(
    public _translate: TranslateService,
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
  }

  onExporting(e: any) {
    e.fileName = this.fileName;
    if (e.format === 'xslx') {
      const workbook = new ExcelJS.Workbook();
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
    this._translate.onLangChange.subscribe((lang) => {
      this._translate
        .get([this.parentKey, this.componentKey])
        .subscribe((translations) => {
          this.breadCrumbItems = [
            { label: translations[this.parentKey] },
            { label: translations[this.componentKey], active: true },
          ];
        });
    });
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

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Formula } from '../models/formula-form-model';
import CustomStore from 'devextreme/data/custom_store';
import { TranslateService } from '@ngx-translate/core';
import { FormulaService } from '../formula.service';
import { ObjectId } from 'bson';

@Component({
  selector: 'app-formula-form',
  templateUrl: './formula-form.component.html',
  styleUrls: ['./formula-form.component.scss'],
})
export class FormulaFormComponent {
  @Input() formula: Formula;
  @Output() closeForm = new EventEmitter<any>();
  @Output() cancelForm = new EventEmitter<any>();

  public modelDataSource: any;
  public rawMaterialDataSource: any;
  public formulaTypeDataSource: any;
  public formulaDataSource: any;

  constructor(
    public _translate: TranslateService,
    private _formulaService: FormulaService
  ) {
    this.modelDataSource = _formulaService.getModelDataSource();
    this.rawMaterialDataSource = _formulaService.getRawMaterialDataSource();
    this.formulaTypeDataSource = _formulaService.getFormulaTypeDataSource();
  }

  onSubmit(e) {
    this.closeForm.emit(this.formula);
  }

  onCancel() {
    this.cancelForm.emit();
  }

  onSaved(e: any, data: any) {
    var values = e.component.getDataSource().items();
    data.setValue(values);
  }

  //Detay gridde yeni kayıt ekleyince devreye giriyor
  onInitNewRow(e: any) {
    var objectId = new ObjectId();
    e.data.id = objectId.toString();
  }
}

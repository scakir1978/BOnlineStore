import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { Formula } from '../models/formula-form-model';
import CustomStore from 'devextreme/data/custom_store';
import { TranslateService } from '@ngx-translate/core';
import { FormulaService } from '../formula.service';
import { ObjectId } from 'bson';
import { FormulaVariableTypes } from '../models/formula-variable-types';
import { forEach } from 'lodash';

@Component({
  selector: 'app-formula-form',
  templateUrl: './formula-form.component.html',
  styleUrls: ['./formula-form.component.scss'],
})
export class FormulaFormComponent implements OnInit {
  @Input() formula: Formula;
  @Output() closeForm = new EventEmitter<any>();
  @Output() cancelForm = new EventEmitter<any>();

  public modelDataSource: any;
  public rawMaterialDataSource: any;
  public formulaTypeDataSource: any;
  public formulaDataSource: any;
  public formulaVariableTypes: FormulaVariableTypes[];

  isModelGridBoxOpened: boolean;
  modelGridValues: string[] = [];

  isRawMaterialGridBoxOpened: boolean;
  rawMaterialGridValues: string[] = [];

  constructor(
    public _translate: TranslateService,
    private _formulaService: FormulaService,
    private ref: ChangeDetectorRef
  ) {
    this.modelDataSource = _formulaService.getRawModelDataSource();
    this.rawMaterialDataSource = _formulaService.getRawMaterialDataSource();
    this.formulaTypeDataSource = _formulaService.getRawFormulaTypeDataSource();
    this.formulaVariableTypes = _formulaService.getFormulaVariableTypes();
    this.formulaDataSource = _formulaService.getRawFormulaDataSource();

    this.onReorder = this.onReorder.bind(this);
    this.onRowChange = this.onRowChange.bind(this);
    this.getFilteredFormulas = this.getFilteredFormulas.bind(this);
    this.executeFormula = this.executeFormula.bind(this);
  }

  ngOnInit(): void {
    this.modelGridValues.push(this.formula.modelId);
    this.rawMaterialGridValues.push(this.formula.rawMaterialId);
  }

  onSubmit(e) {
    this.formula.modelId = this.modelGridValues[0];
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

  onReorder(e) {
    const visibleRows = e.component.getVisibleRows();
    const toIndex = this.formula.formulaDetails.findIndex(
      (item) => item.id === visibleRows[e.toIndex].data.id
    );
    const fromIndex = this.formula.formulaDetails.findIndex(
      (item) => item.id === e.itemData.id
    );

    this.formula.formulaDetails.splice(fromIndex, 1);
    this.formula.formulaDetails.splice(toIndex, 0, e.itemData);

    this.formula.formulaText = this._formulaService.generateFormulaText(
      this.formula.formulaDetails
    );
  }

  editorPreparing(e) {
    if (e.parentType === 'dataRow' && e.dataField === 'variableValue') {
      e.editorOptions.disabled = true;
      e.editorOptions.disabled = false;
    }

    if (e.parentType === 'dataRow' && e.dataField === 'formulId') {
      e.editorOptions.disabled = true;
      if (e.row.data && e.row.data.variableType === 'RESULTVARIABLE')
        e.editorOptions.disabled = false;
    }
  }

  onRowChange(e) {
    this.formula.formulaText = this._formulaService.generateFormulaText(
      this.formula.formulaDetails
    );
  }

  getFilteredFormulas(options) {
    return {
      store: this.formulaDataSource,
      filter: options.data ? ['modelId', '=', this.formula.modelId] : null,
    };
  }

  async executeFormula(e) {
    var result = await this._formulaService.executeFormula(
      this.formula.formulaDetails
    );
    return !!result;
  }

  usageAmountGreaterThanZero(e) {
    var usageAmount = parseFloat(e.value.toString());
    return usageAmount > 0;
  }

  onModelGridOptionChanged(e) {
    if (e.name === 'value') {
      this.isModelGridBoxOpened = false;
      this.ref.detectChanges();
    }
  }

  onRawMaterialGridOptionChanged(e) {
    if (e.name === 'value') {
      this.isRawMaterialGridBoxOpened = false;
      this.ref.detectChanges();
    }
  }
}

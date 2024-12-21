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
import Swal from 'sweetalert2';

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
  public panelDataSource: any;
  public rawMaterialDataSource: any;
  public formulaTypeDataSource: any;
  public formulaDataSource: any;
  public formulaVariableTypes: FormulaVariableTypes[];

  isModelGridBoxOpened: boolean;
  modelGridValues: string[] = [];

  isPanelGridBoxOpened: boolean;
  panelGridValues: string[] = [];

  isRawMaterialGridBoxOpened: boolean;
  rawMaterialGridValues: string[] = [];

  constructor(
    public _translate: TranslateService,
    private _formulaService: FormulaService,
    private ref: ChangeDetectorRef
  ) {
    this.modelDataSource = _formulaService.getRawModelDataSource();
    this.panelDataSource = _formulaService.getRawPanelDataSource();
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
    this.panelGridValues.push(this.formula.panelId);
    this.rawMaterialGridValues.push(this.formula.rawMaterialId);
  }

  onSubmit(e) {
    if (this.modelGridValues && this.modelGridValues[0])
      this.formula.modelId = this.modelGridValues[0];
    else this.formula.modelId = null;

    if (this.panelGridValues && this.panelGridValues[0])
      this.formula.panelId = this.panelGridValues[0];
    else this.formula.panelId = null;

    this.formula.rawMaterialId = this.rawMaterialGridValues[0];
    this.closeForm.emit(this.formula);
  }

  onCancel() {
    this.cancelForm.emit();
  }

  //Detay gridde yeni kayıt ekleyince devreye giriyor
  onInitNewRow(e: any) {
    if (this.modelGridValues[0] && this.panelGridValues[0]) {
      this.showModelIdAndPanelIdCannotBeBothSetMessage().then(() => {
        window.setTimeout(function () {
          e.component.cancelEditData();
        }, 0);
        return;
      });
    }

    var objectId = new ObjectId();
    e.data.id = objectId.toString();
  }

  showModelIdAndPanelIdCannotBeBothSetMessage() {
    return Swal.fire({
      title: this._translate.instant('ERROR'),
      text: this._translate.instant('MODELIDANDPANELIDCANNOTBEBOTHSET'),
      icon: 'error',
      confirmButtonColor: '#364574',
      confirmButtonText: this._translate.instant('OK'),
    });
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
    if (this.modelGridValues[0]) {
      return {
        store: this.formulaDataSource,
        filter: options.data ? ['modelId', '=', this.modelGridValues[0]] : null,
      };
    } else if (this.panelGridValues[0]) {
      return {
        store: this.formulaDataSource,
        filter: options.data ? ['panelId', '=', this.panelGridValues[0]] : null,
      };
    }
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

  onPanelGridOptionChanged(e) {
    if (e.name === 'value') {
      this.isPanelGridBoxOpened = false;
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

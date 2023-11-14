import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { FormulaService } from './formula.service';
import { Component, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FormCrudTypeEnum } from 'app/base-classes/base-enums/form-crud-type.enum';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { ObjectId } from 'bson';
import { Formula } from './models/formula-form-model';
import Swal from 'sweetalert2';

@Component({
  selector: 'formula',
  templateUrl: './formula.component.html',
  styleUrls: ['./formula.component.scss'],
})
export class FormulaComponent extends BaseDefinitionsOnGridComponent {
  public formulaDataSource: DataSource;
  public modelDataSource: CustomStore;
  public rawMaterialDataSource: CustomStore;
  public formulaTypeDataSource: CustomStore;

  public formActive: boolean = false;
  public formCrudType: FormCrudTypeEnum;
  public formula: Formula;

  //Popup properties
  public popupVisible = false;
  public copyModelId: string;
  public copyFormulaId: string;
  public copyFormulaCode: string;
  public copyFormulaName: string;

  copyButtonOptions: any;
  closeButtonOptions: any;

  constructor(
    public override _translate: TranslateService,
    private _formulaService: FormulaService
  ) {
    super(
      _translate,
      'FORMULA', //Pdf, excel dosya adı
      'FORMULA', //breadCrump için kullanılacak componenet keyi
      'PRODUCTION' //breadCrump için kullanılacak componenetin bağlı olduğu parent menü
    );
    this.formulaDataSource = _formulaService.getDataSource();
    this.modelDataSource = _formulaService.getRawModelDataSource();
    this.rawMaterialDataSource = _formulaService.getRawMaterialDataSource();
    this.formulaTypeDataSource = _formulaService.getRawFormulaTypeDataSource();

    this.removeFormula = this.removeFormula.bind(this);
    this.editFormula = this.editFormula.bind(this);
    this.submitForm = this.submitForm.bind(this);
    this.showCopyFormulaPopup = this.showCopyFormulaPopup.bind(this);

    this.formActive = false;
    this.popupVisible = false;
  }

  newFormula(e) {
    var objectId = new ObjectId();
    this.formActive = true;
    this.formula = new Formula(objectId.toString());
    this.formCrudType = FormCrudTypeEnum.INSERT;
  }

  async editFormula(e) {
    this.formCrudType = FormCrudTypeEnum.UPDATE;
    this.formula = await this._formulaService.getById(e.row.data.id);
    this.formActive = true;
    e.event.preventDefault();
  }

  removeFormula(e) {
    this._formulaService.delete(e.row.data.id);
    this.refreshDataGrid();
    e.event.preventDefault();
  }

  cancelForm() {
    this.formActive = false;
  }

  async submitForm(formula: Formula) {
    if (this.formCrudType == FormCrudTypeEnum.INSERT) {
      try {
        await this._formulaService.insert(formula);
      } catch (error) {
        this.showErrorMessage(error.message);
        return;
      }
    }
    if (this.formCrudType == FormCrudTypeEnum.UPDATE) {
      try {
        await this._formulaService.update(formula, formula.id);
      } catch (error) {
        this.showErrorMessage(error.message);
        return;
      }
    }

    this.formActive = false;
    this.refreshDataGrid();
  }

  async copyFormula(e) {
    try {
      await this._formulaService.copyFormula(
        this.copyFormulaId,
        this.copyFormulaCode,
        this.copyModelId
      );
      this.showSuccessMessage(this._translate.instant('COPYFORMULASUCCESS'));
      this.popupVisible = false;
      this.refreshDataGrid();
    } catch (error) {
      this.showErrorMessage(error.message);
    }
  }

  cancelCopyFormula(e) {
    this.popupVisible = false;
  }

  showCopyFormulaPopup(e) {
    this.copyModelId = null;
    this.copyFormulaId = e.row.data.id;
    this.copyFormulaCode = e.row.data.code;
    this.copyFormulaName = e.row.data.name;
    this.popupVisible = true;
  }

  showErrorMessage(errorMessage) {
    Swal.fire({
      title: this._translate.instant('SAVERECORDERROR'),
      text: errorMessage,
      icon: 'error',
      confirmButtonColor: '#364574',
      confirmButtonText: this._translate.instant('OK'),
    });
  }

  showSuccessMessage(successMessage) {
    Swal.fire({
      title: this._translate.instant('INFORMATION'),
      text: successMessage,
      icon: 'success',
      confirmButtonColor: '#364574',
      confirmButtonText: this._translate.instant('OK'),
    });
  }
}

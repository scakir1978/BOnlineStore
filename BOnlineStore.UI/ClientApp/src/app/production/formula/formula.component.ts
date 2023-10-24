import { BaseDefinitionsOnGridComponent } from '../../base-classes/base-definitions-on-grid/base-definitions-on-grid.component';
import { FormulaService } from './formula.service';
import { Component } from '@angular/core';
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
    this.modelDataSource = _formulaService.getModelDataSource();
    this.rawMaterialDataSource = _formulaService.getRawMaterialDataSource();
    this.formulaTypeDataSource = _formulaService.getFormulaTypeDataSource();

    this.removeFormula = this.removeFormula.bind(this);
    this.editFormula = this.editFormula.bind(this);
    this.submitForm = this.submitForm.bind(this);

    this.formActive = false;
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

  async submitForm(formula: Formula) {
    if (this.formCrudType == FormCrudTypeEnum.INSERT) {
      try {
        await this._formulaService.insert(formula);
      } catch (error) {
        console.log(error);
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

  cancelForm() {
    this.formActive = false;
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
}

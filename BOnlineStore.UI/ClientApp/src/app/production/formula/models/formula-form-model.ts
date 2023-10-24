import { FormulaSortEnum } from '../enums/formula-sort.enum';
import { IFormula, IFormulaDetail } from './formula-form-interface';

export class Formula implements IFormula {
  constructor(id: string) {
    this.id = id;
    this.formulaDetails = [];
  }

  /* Formül tanımı id */
  id: string | null;

  /** Formül kodu */
  code: string | null;

  /** Formül Adı */
  name: string | null;

  /* Model */
  modelId: string | null;

  /* Hammadde */
  rawMaterialId: string | null;

  /* Formül tipi */
  formulaTypeId: string | null;

  /* Kullanım miktarı */
  usageAmount: number | null;

  /* Matematiksel olarak oluşan formül */
  formulaText: string | null;

  /* Formül türü */
  formulaSort: FormulaSortEnum | null;

  /* Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir. */
  formulaDetails: IFormulaDetail[] | null;
}

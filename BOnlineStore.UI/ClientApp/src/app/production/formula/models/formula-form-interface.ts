import { FormulaSortEnum } from '../../../enums/production/formula-sort.enum';

/* Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir. */
export interface IFormulaDetail {
  /* Formül detay id */
  id: string | null;

  /** "(",")" parantez açma, kapama, dört işlem "+,-,*,/" ve değişkenler "WIDTH1, WIDTH2, WIDTH3, HEIGHT, RESULTVARIABLE" ile sayısal değer içeren (1,5,20, 150) ifadelerinden biri olabilir. */
  variableType: string | null;

  /** Eğer değişten tipi (VariableType) CONSTANT ise, Bu alana sayısal bir bilgi girilir. */
  variableValue: number | null;

  /** Eğer değişten tipi (VariableType) RESULTVARIABLE ise, formülde başka bir formül hesabının sonucu kullanılacak demektir. O yüzden formul id bilgisi bu alana kayıt edilir. */
  formulId: string | null;
}

export interface IFormula {
  /* Formül tanımı id */
  id: string | null;

  /** Formül kodu */
  code: string | null;

  /** Formül Adı */
  name: string | null;

  /* Model */
  modelId: string | null;

  /* Panel */
  panelId: string | null;

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

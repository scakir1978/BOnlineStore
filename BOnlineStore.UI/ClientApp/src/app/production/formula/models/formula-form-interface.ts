import { FormulaSortEnum } from '../enums/formula-sort.enum';

/* Formülü oluşturan unsurların, düzgün matematiksel bir ifadeye dönülmesi için buradaki detaylara bilgi girilir. */
export interface IFormulaDetail {
  /* Formül detay id */
  id: string | null;

  /** "(",")" parantez açma, kapama, dört işlem "+,-,*,/" ve değişkenler "EN1, EN2, EN3, YUKSEKLIK, SONUCDEGISKENI" ile sayısal değer içeren (1,5,20, 150) ifadelerinden biri olabilir. */
  variableType: string | null;

  /** Eğer değişten tipi (VariableType) SABIT ise, Bu alana sayısal bir bilgi girilir. */
  variableValue: number | null;

  /** Eğer değişten tipi (VariableType) SONUCDEGISKENI ise, formülde başka bir formül hesabının sonucu kullanılacak demektir. O yüzden formul id bilgisi bu alana kayıt edilir. */
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

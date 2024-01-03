import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface RawMaterialDto extends EntityDto {
  code: string;
  name: string;
  baseGroupId: string | null;
  groupId: string | null;
  criticalAmount: number | null;
  transferDate: string | null;
  vatRatio: number | null;
  salePrice01: number | null;
  salePrice02: number | null;
  salePrice03: number | null;
  stockSubDetail: number | null;
  purchasePrice01: number | null;
  purchasePrice02: number | null;
  stockUnit01Id: string | null;
  stockUnit02Id: string | null;
  stockUnit03Id: string | null;
  stockUnitTransformationRatio01: number | null;
  stockUnitTransformationRatio02: number | null;
  stockUnitTransformationRatio03: number | null;
  productUnit01Id: string | null;
  productUnit02Id: string | null;
  productUnit03Id: string | null;
  productUnitTransformationRatio01: number | null;
  productUnitTransformationRatio02: number | null;
  productUnitTransformationRatio03: number | null;
}

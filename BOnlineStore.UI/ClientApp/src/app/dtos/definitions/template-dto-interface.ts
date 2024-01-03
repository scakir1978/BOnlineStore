import { EntityDto } from '../../base-classes/base-interfaces/entity-dto-interface';

export interface TemplateDto extends EntityDto {
  code: string;
  name: string;
}

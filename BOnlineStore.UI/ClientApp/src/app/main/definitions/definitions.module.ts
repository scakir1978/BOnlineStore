import { ColorModule } from './color/color.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';

import { CoreCommonModule } from '@core/common.module';
import { ContentHeaderModule } from 'app/layout/components/content-header/content-header.module';
import { ModelGroupModule } from './model-group/model-group.module';
import { ColorGroupModule } from './color-group/color-group.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CoreCommonModule,
    ContentHeaderModule,
    NgbModule,
    NgSelectModule,
    FormsModule,
    ModelGroupModule,
    ColorGroupModule,
    ColorModule,
  ],
  providers: [],
})
export class DefinitionsModule {}

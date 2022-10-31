import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {
  PerfectScrollbarConfigInterface,
  PerfectScrollbarModule,
  PERFECT_SCROLLBAR_CONFIG,
} from 'ngx-perfect-scrollbar';

import { CoreDirectivesModule } from '@core/directives/directives';
import { CoreSidebarModule } from '@core/components/core-sidebar/core-sidebar.module';

import { CoreThemeCustomizerComponent } from '@core/components/theme-customizer/theme-customizer.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
  wheelPropagation: false,
};

@NgModule({
  declarations: [CoreThemeCustomizerComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    PerfectScrollbarModule,
    CoreDirectivesModule,
    CoreSidebarModule,
    TranslateModule,
  ],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG,
    },
    TranslateService,
  ],
  exports: [CoreThemeCustomizerComponent, TranslateModule],
})
export class CoreThemeCustomizerModule {}

/*exports: [TranslateModule],
  providers: [ModelGroupService, TranslateService]*/

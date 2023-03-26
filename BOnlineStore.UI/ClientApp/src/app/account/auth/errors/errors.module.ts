import { TranslateService } from '@ngx-translate/core';
import { TranslateModule } from '@ngx-translate/core';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

// Load Icons
import { defineElement } from 'lord-icon-element';
import lottie from 'lottie-web';

// Component
import { Error404RoutingModule } from './errors-routing.module';
import { BasicComponent } from './basic/basic.component';
import { CoverComponent } from './cover/cover.component';
import { AltComponent } from './alt/alt.component';
import { Page500Component } from './page500/page500.component';
import { OfflineComponent } from './offline/offline.component';
import { Page401Component } from './page401/page401.component';

@NgModule({
  declarations: [
    BasicComponent,
    CoverComponent,
    AltComponent,
    Page500Component,
    Page401Component,
    OfflineComponent,
  ],
  imports: [CommonModule, Error404RoutingModule, TranslateModule],
  exports: [TranslateModule],
  providers: [TranslateService],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ErrorsModule {
  constructor() {
    defineElement(lottie.loadAnimation);
  }
}

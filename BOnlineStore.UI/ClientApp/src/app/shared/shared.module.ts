import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  NgbNavModule,
  NgbAccordionModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';

// Counter
//import { CountToModule } from 'angular-count-to';

import { BreadcrumbsComponent } from './breadcrumbs/breadcrumbs.component';

import { ScrollspyDirective } from './scrollspy.directive';

//Printing
import { NgxPrintModule } from 'ngx-print';

@NgModule({
  declarations: [BreadcrumbsComponent, ScrollspyDirective],
  imports: [
    CommonModule,
    NgbNavModule,
    NgbAccordionModule,
    NgbDropdownModule,
    //CountToModule,
    NgxPrintModule,
  ],
  exports: [BreadcrumbsComponent, ScrollspyDirective],
})
export class SharedModule {}

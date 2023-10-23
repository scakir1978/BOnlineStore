import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { FormulaComponent } from './formula.component';
import { FormulaService } from './formula.service';

const routes: Routes = [
  {
    path: 'formula',
    component: FormulaComponent,
    canActivate: [AuthGuard],
    resolve: {
        Formula: FormulaService,
    },
  },
];

@NgModule({
  declarations: [FormulaComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [FormulaService, TranslateService],
})
export class FormulaModule {}

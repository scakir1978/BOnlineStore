import { SharedModule } from '../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from '../../core/guards/auth.guard';
import { AssemblerComponent } from './assembler.component';
import { AssemblerService } from './assembler.service';

const routes: Routes = [
  {
    path: 'assembler',
    component: AssemblerComponent,
    canActivate: [AuthGuard],
    resolve: {
      Assembler: AssemblerService,
    },
  },
];

@NgModule({
  declarations: [AssemblerComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [AssemblerService, TranslateService],
})
export class AssemblerModule {}

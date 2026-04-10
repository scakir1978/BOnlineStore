import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RoleComponent } from './role.component';
import { RoleService } from './role.service';

const routes: Routes = [
  {
    path: 'role',
    component: RoleComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  declarations: [RoleComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [RoleService, TranslateService],
})
export class RoleModule {}

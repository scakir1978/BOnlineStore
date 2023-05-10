import { SharedModule } from './../../shared/shared.module';
import { DevExtremeModule } from 'devextreme-angular';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { AuthGuard } from './../../core/guards/auth.guard';
import { RecipeTypeComponent } from './recipe-type.component';
import { RecipeTypeService } from './recipe-type.service';

const routes: Routes = [
  {
    path: 'recipe-type',
    component: RecipeTypeComponent,
    canActivate: [AuthGuard],
    resolve: {
        RecipeType: RecipeTypeService,
    },
  },
];

@NgModule({
  declarations: [RecipeTypeComponent],
  imports: [
    RouterModule.forChild(routes),
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [RecipeTypeService, TranslateService],
})
export class RecipeTypeModule {}

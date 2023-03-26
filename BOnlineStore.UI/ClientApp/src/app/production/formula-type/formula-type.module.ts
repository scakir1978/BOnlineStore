import { SharedModule } from "./../../shared/shared.module";
import { DevExtremeModule } from "devextreme-angular";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule, TranslateService } from "@ngx-translate/core";
import { AuthGuard } from "./../../core/guards/auth.guard";

import { FormulaTypeComponent } from "./formula-type.component";
import { FormulaTypeService } from "./formula-type.service";

const routes: Routes = [
  {
    path: "formula-type",
    component: FormulaTypeComponent,
    canActivate: [AuthGuard],
    resolve: {
      FormulaType: FormulaTypeService,
    },
    data: { animation: "formula-type" },
  },
];

@NgModule({
  declarations: [FormulaTypeComponent],
  imports: [
    RouterModule.forChild(routes),
    NgbModule,
    TranslateModule,
    DevExtremeModule,
    SharedModule,
  ],
  exports: [TranslateModule],
  providers: [FormulaTypeService, TranslateService],
})
export class FormulaTypeModule {}

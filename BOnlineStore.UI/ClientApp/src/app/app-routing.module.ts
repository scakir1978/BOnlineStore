import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { LayoutComponent } from "./layouts/layout.component";

// Auth
import { AuthGuard } from "./core/guards/auth.guard";

const routes: Routes = [
  {
    path: "",
    component: LayoutComponent,
    loadChildren: () =>
      import("./pages/pages.module").then((m) => m.PagesModule),
    canActivate: [AuthGuard],
  },
  {
    path: "auth",
    loadChildren: () =>
      import("./account/account.module").then((m) => m.AccountModule),
  },
  {
    path: "identity",
    component: LayoutComponent,
    loadChildren: () =>
      import("./callback/callback.module").then((m) => m.CallbackModule),
  },
  {
    path: "definitions",
    component: LayoutComponent,
    loadChildren: () =>
      import("./definitions/definitions.module").then(
        (m) => m.DefinitionsModule
      ),
  },
  {
    path: "production",
    component: LayoutComponent,
    loadChildren: () =>
      import("./production/production.module").then((m) => m.ProductionModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

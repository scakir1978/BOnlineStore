import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CallbackComponent } from './callback.component';

const routes: Routes = [
  {
    path: 'callback',
    component: CallbackComponent,
    data: { animation: 'callback' }
  },
  {
    path: 'callout',
    component: CallbackComponent,
    data: { animation: 'callout' }
  },
  {
    path: 'silent',
    component: CallbackComponent,
    data: { animation: 'silent' }
  }
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ]
})
export class CallbackModule { }

import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ToastService } from './toast-service';

import { circle, latLng, tileLayer } from 'leaflet';
import { SwiperOptions } from 'swiper';

import { BestSelling, TopSelling, RecentSelling, statData } from './data';
import { ChartType } from './dashboard.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

/**
 * Ecommerce Component
 */
export class DashboardComponent implements OnInit {

  // bread crumb items
  breadCrumbItems!: Array<{}>;

  constructor() {

  }

  ngOnInit(): void {
    /**
     * BreadCrumb
     */
    this.breadCrumbItems = [
      { label: 'Pages' },
      { label: 'Starter', active: true }
    ];

  }

}
import { LayoutService } from './../core/services/layout.service';
import { Component, OnInit } from '@angular/core';

import { EventService } from '../core/services/event.service';
import {
  LAYOUT_VERTICAL,
  LAYOUT_HORIZONTAL,
  LAYOUT_TWOCOLUMN,
  LAYOUT_MODE,
  LAYOUT_WIDTH,
  LAYOUT_POSITION,
  SIDEBAR_SIZE,
  SIDEBAR_COLOR,
  TOPBAR,
} from './layout.model';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})

/**
 * Layout Component
 */
export class LayoutComponent implements OnInit {
  layoutType!: string;

  constructor(
    private eventService: EventService,
    private layoutService: LayoutService
  ) {}

  ngOnInit(): void {
    this.layoutType = LAYOUT_VERTICAL;

    var layoutSettings = this.layoutService.getLayoutSettings();
    if (layoutSettings && layoutSettings.layoutType) {
      this.layoutType = layoutSettings.layoutType;
    }
    // listen to event and change the layout, theme, etc
    this.eventService.subscribe('changeLayout', (layout) => {
      this.layoutType = layout;
    });
  }

  /**
   * Check if the vertical layout is requested
   */
  isVerticalLayoutRequested() {
    return this.layoutType === 'vertical';
  }

  /**
   * Check if the semibox layout is requested
   */
  isSemiboxLayoutRequested() {
    return this.layoutType === 'semibox';
  }

  /**
   * Check if the horizontal layout is requested
   */
  isHorizontalLayoutRequested() {
    return this.layoutType === 'horizontal';
  }

  /**
   * Check if the horizontal layout is requested
   */
  isTwoColumnLayoutRequested() {
    return this.layoutType === 'twocolumn';
  }
}

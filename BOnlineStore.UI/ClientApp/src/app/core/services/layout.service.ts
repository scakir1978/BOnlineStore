import { Injectable } from '@angular/core';
import { LayoutSettings } from '../models/layout.service.models';

@Injectable({ providedIn: 'root' })
export class LayoutService {
  constructor() {}

  setLayoutSettings(
    layoutType: string,
    layoutMode: string,
    layoutWidth: string,
    layoutPosition: string,
    topBar: string,
    sideBarSize: string,
    sideBarView: string,
    sideBarColor: string,
    dataPreLoader: string,
    sideBarVisibility: string,
    bodyImage: string
  ) {
    var layoutSettings: LayoutSettings = {
      bodyImage: bodyImage,
      dataPreLoader: dataPreLoader,
      layoutMode: layoutMode,
      layoutPosition: layoutPosition,
      layoutType: layoutType,
      layoutWidth: layoutWidth,
      sideBarColor: sideBarColor,
      sideBarSize: sideBarSize,
      sideBarView: sideBarView,
      sideBarVisibility: sideBarVisibility,
      topBar: topBar,
    };

    localStorage.setItem('layoutSettings', JSON.stringify(layoutSettings));
  }

  setLayoutModeFromTopbar(mode: string) {
    var layoutSettings = this.getLayoutSettings();
    layoutSettings.layoutMode = mode;
    localStorage.setItem('layoutSettings', JSON.stringify(layoutSettings));
  }

  getLayoutSettings(): LayoutSettings {
    var layoutSettings: LayoutSettings = JSON.parse(
      localStorage.getItem('layoutSettings')!
    );

    if (!layoutSettings) {
      layoutSettings = {
        bodyImage: 'none',
        dataPreLoader: 'disable',
        layoutMode: 'dark',
        layoutPosition: 'fixed',
        layoutType: 'vertical',
        layoutWidth: 'fluid',
        sideBarColor: 'dark',
        sideBarSize: 'lg',
        sideBarView: 'default',
        sideBarVisibility: 'show',
        topBar: 'light',
      };
      localStorage.setItem('layoutSettings', JSON.stringify(layoutSettings));
    }

    return layoutSettings;
  }

  setLayoutSettingsToDocument(
    layoutType: string,
    document: Document,
    layoutSettings: LayoutSettings
  ) {
    document.documentElement.setAttribute('data-layout', layoutType);

    document.documentElement.setAttribute(
      'data-topbar',
      layoutSettings.topBar!
    );

    document.documentElement.setAttribute(
      'data-sidebar',
      layoutSettings.sideBarColor!
    );

    document.documentElement.setAttribute(
      'data-layout-style',
      layoutSettings.sideBarView!
    );

    document.documentElement.setAttribute(
      'data-layout-mode',
      layoutSettings.layoutMode!
    );

    document.documentElement.setAttribute(
      'data-layout-width',
      layoutSettings.layoutWidth!
    );

    document.documentElement.setAttribute(
      'data-layout-position',
      layoutSettings.layoutPosition!
    );

    document.documentElement.setAttribute(
      'data-preloader',
      layoutSettings.dataPreLoader!
    );

    document.documentElement.setAttribute(
      'data-body-image',
      layoutSettings.bodyImage!
    );

    document.documentElement.setAttribute(
      'data-sidebar-visibility',
      layoutSettings.sideBarVisibility!
    );

    document.documentElement.setAttribute(
      'data-sidebar-size',
      layoutSettings.sideBarSize!
    );
  }
}

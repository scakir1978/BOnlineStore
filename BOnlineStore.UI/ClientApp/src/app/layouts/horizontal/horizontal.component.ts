import { LayoutService } from './../../core/services/layout.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-horizontal',
  templateUrl: './horizontal.component.html',
  styleUrls: ['./horizontal.component.scss'],
})

/**
 * Horizontal Component
 */
export class HorizontalComponent implements OnInit {
  constructor(private layoutService: LayoutService) {}

  isCondensed = false;

  ngOnInit(): void {
    this.layoutService.setLayoutSettingsToDocument(
      'horizontal',
      document,
      this.layoutService.getLayoutSettings()
    );

    //this.changeLoader(this.layoutService.getLayoutSettings().dataPreLoader);

    /*document.documentElement.setAttribute("data-layout", "horizontal");
    document.documentElement.setAttribute("data-topbar", "light");
    document.documentElement.setAttribute("data-sidebar", "dark");
    document.documentElement.setAttribute("data-layout-style", "default");
    document.documentElement.setAttribute("data-bs-theme", "dark");
    document.documentElement.setAttribute("data-layout-width", "fluid");
    document.documentElement.setAttribute("data-layout-position", "fixed");
    document.documentElement.setAttribute("data-preloader", "disable");
    document.documentElement.setAttribute("data-body-image", "img-3");*/
  }

  /**
   * on settings button clicked from topbar
   */
  onSettingsButtonClicked() {
    document.body.classList.toggle('right-bar-enabled');
    const rightBar = document.getElementById('theme-settings-offcanvas');
    if (rightBar != null) {
      rightBar.classList.toggle('show');
      rightBar.setAttribute('style', 'visibility: visible;');
    }
  }

  /**
   * On mobile toggle button clicked
   */
  onToggleMobileMenu() {
    if (document.documentElement.clientWidth <= 1024) {
      document.body.classList.toggle('menu');
    }
  }

  /*changeLoader(loader: string) {
    document.documentElement.setAttribute('data-preloader', loader);
    var preloader = document.getElementById('preloader');
    if (preloader) {
      setTimeout(function () {
        (document.getElementById('preloader') as HTMLElement).style.opacity =
          '0';
        (document.getElementById('preloader') as HTMLElement).style.visibility =
          'hidden';
      }, 1000);
    }
  }*/
}

import { EventService } from './../../core/services/event.service';
import { LayoutService } from './../../core/services/layout.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-two-column',
  templateUrl: './two-column.component.html',
  styleUrls: ['./two-column.component.scss'],
})

/**
 * TwoColumnComponent
 */
export class TwoColumnComponent implements OnInit {
  constructor(
    private layoutService: LayoutService,
    private eventService: EventService
  ) {}
  isCondensed = false;

  ngOnInit(): void {
    //console.log('layout =>', document.documentElement.getAttribute('data-layout'))

    this.layoutService.setLayoutSettingsToDocument(
      'twocolumn',
      document,
      this.layoutService.getLayoutSettings()
    );

    //this.changeLoader(this.layoutService.getLayoutSettings().dataPreLoader);

    /*document.documentElement.setAttribute('data-layout', 'twocolumn');
    document.documentElement.setAttribute('data-topbar', 'light');
    document.documentElement.setAttribute('data-sidebar', 'dark');
    document.documentElement.setAttribute('data-layout-style', 'default');
    document.documentElement.setAttribute('data-layout-mode', 'dark');
    document.documentElement.setAttribute('data-layout-width', 'fluid');
    document.documentElement.setAttribute('data-layout-position', 'fixed');
    document.documentElement.setAttribute('data-preloader', 'disable');
    document.documentElement.setAttribute('data-body-image', 'img-3');*/

    window.addEventListener('resize', function () {
      if (document.documentElement.getAttribute('data-layout') == 'twocolumn') {
        if (document.documentElement.clientWidth <= 767) {
          document.documentElement.setAttribute('data-layout', 'vertical');
          document.getElementById('side-bar')?.classList.remove('d-none');
        } else {
          document.documentElement.setAttribute('data-layout', 'twocolumn');
          document.getElementById('side-bar')?.classList.add('d-none');
        }
      }
    });
  }

  onResize(event: any) {
    if (document.body.getAttribute('layout') == 'twocolumn') {
      if (event.target.innerWidth <= 767) {
        this.eventService.broadcast('changeLayout', 'vertical');
      } else {
        this.eventService.broadcast('changeLayout', 'twocolumn');
        document.body.classList.remove('twocolumn-panel');
        document.body.classList.remove('vertical-sidebar-enable');
      }
    }
  }

  /**
   * On mobile toggle button clicked
   */
  onToggleMobileMenu() {
    if (document.documentElement.clientWidth <= 767) {
      document.body.classList.toggle('vertical-sidebar-enable');
    } else {
      document.body.classList.toggle('twocolumn-panel');
    }
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

  isTwoColumnLayoutRequested() {
    return 'twocolumn' === document.documentElement.getAttribute('data-layout');
  }

  issemiboxLayoutRequested() {
    return 'semibox' === document.documentElement.getAttribute('data-layout');
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

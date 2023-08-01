import { DOCUMENT } from '@angular/common';
import { LayoutService } from './../../core/services/layout.service';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-vertical',
  templateUrl: './vertical.component.html',
  styleUrls: ['./vertical.component.scss'],
})
export class VerticalComponent implements OnInit {
  isCondensed = false;

  constructor(private layoutService: LayoutService) {}

  ngOnInit(): void {
    this.layoutService.setLayoutSettingsToDocument(
      'vertical',
      document,
      this.layoutService.getLayoutSettings()
    );

    this.changeLoader(this.layoutService.getLayoutSettings().dataPreLoader);

    window.addEventListener('resize', function () {
      if (document.documentElement.clientWidth <= 767) {
        document.documentElement.setAttribute('data-sidebar-size', '');
        document.querySelector('.hamburger-icon')?.classList.add('open');
      } else if (document.documentElement.clientWidth <= 1024) {
        document.documentElement.setAttribute('data-sidebar-size', 'sm');
        document.querySelector('.hamburger-icon')?.classList.add('open');
      } else if (document.documentElement.clientWidth >= 1024) {
        document.documentElement.setAttribute('data-sidebar-size', 'lg');
        document.querySelector('.hamburger-icon')?.classList.remove('open');
      }
    });
  }

  /**
   * On mobile toggle button clicked
   */
  onToggleMobileMenu() {
    const currentSIdebarSize =
      document.documentElement.getAttribute('data-sidebar-size');
    if (document.documentElement.clientWidth >= 767) {
      if (currentSIdebarSize == null) {
        document.documentElement.getAttribute('data-sidebar-size') == null ||
        document.documentElement.getAttribute('data-sidebar-size') == 'lg'
          ? document.documentElement.setAttribute('data-sidebar-size', 'sm')
          : document.documentElement.setAttribute('data-sidebar-size', 'lg');
      } else if (currentSIdebarSize == 'md') {
        document.documentElement.getAttribute('data-sidebar-size') == 'md'
          ? document.documentElement.setAttribute('data-sidebar-size', 'sm')
          : document.documentElement.setAttribute('data-sidebar-size', 'md');
      } else {
        document.documentElement.getAttribute('data-sidebar-size') == 'sm'
          ? document.documentElement.setAttribute('data-sidebar-size', 'lg')
          : document.documentElement.setAttribute('data-sidebar-size', 'sm');
      }
    }

    if (document.documentElement.clientWidth <= 767) {
      document.body.classList.toggle('vertical-sidebar-enable');
    }
    this.isCondensed = !this.isCondensed;
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

  changeLoader(loader: string) {
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
  }
}

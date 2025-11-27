import {
  Component,
  OnInit,
  EventEmitter,
  Output,
  ViewChild,
  ElementRef,
  OnDestroy,
} from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { EventService } from '../../core/services/event.service';
import { MenuAuthorizationService } from '../../core/services/menu-authorization.service';

import { MENU } from '././../../menu/menu';
import { MenuItem } from './../../menu/menu.model';

//import { MENU } from "./menu";
//import { MenuItem } from "./menu.model";

@Component({
  selector: 'app-two-column-sidebar',
  templateUrl: './two-column-sidebar.component.html',
  styleUrls: ['./two-column-sidebar.component.scss'],
})
export class TwoColumnSidebarComponent implements OnInit, OnDestroy {
  menu: any;
  toggle: any = true;
  menuItems: MenuItem[] = [];
  @ViewChild('sideMenu') sideMenu!: ElementRef;
  @Output() mobileMenuButtonClicked = new EventEmitter();

  currentBackground: string = 'light';
  private sideBarBackgroundSubscription: any;

  constructor(
    private router: Router,
    public translate: TranslateService,
    private eventService: EventService,
    private menuAuthService: MenuAuthorizationService
  ) {
    translate.setDefaultLang('tr');
  }

  ngOnInit(): void {
    // Menu Items - Rol bazlı filtreleme ile
    this.menuItems = this.menuAuthService.filterMenuByRole(MENU);
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.initActiveMenu();
      }
    });

    // Initialize theme from current document attribute
    const currentBackground =
      document.documentElement.getAttribute('data-sidebar') || 'light';
    this.currentBackground = currentBackground;

    // Listen for theme change events
    this.sideBarBackgroundSubscription = this.eventService.subscribe(
      'changeSideBarBackground',
      (currentBackground: string) => {
        this.currentBackground = currentBackground;
      }
    );
  }

  /***
   * Activate drop down set
   */
  ngAfterViewInit() {
    setTimeout(() => {
      this.initActiveMenu();
    }, 0);
  }

  ngOnDestroy(): void {
    if (this.sideBarBackgroundSubscription) {
      this.sideBarBackgroundSubscription.unsubscribe();
    }
  }

  /**
   * Get logo path based on current theme
   */
  getLogoPath(): string {
    return this.currentBackground === 'dark'
      ? 'assets/images/logo-console-log-dark2.png'
      : 'assets/images/logo-console-log-light3.png';
  }

  getIconPath(): string {
    return this.currentBackground === 'dark'
      ? 'assets/images/console-log-icon.png'
      : 'assets/images/console-log-icon-light.png';
  }

  toggleSubItem(event: any) {
    let isCurrentMenuId = event.target.closest('a.nav-link');
    let isMenu = isCurrentMenuId.nextElementSibling as any;
    if (isMenu.classList.contains('show')) {
      isMenu.classList.remove('show');
      isCurrentMenuId.setAttribute('aria-expanded', 'false');
    } else {
      let dropDowns = Array.from(
        document.querySelectorAll('.menu-dropdown .show')
      );
      dropDowns.forEach((node: any) => {
        node.classList.remove('show');
      });

      let subDropDowns = Array.from(
        document.querySelectorAll('.menu-dropdown .nav-link')
      );
      subDropDowns.forEach((submenu: any) => {
        submenu.setAttribute('aria-expanded', 'false');
      });

      if (event.target && event.target.nextElementSibling) {
        isCurrentMenuId.setAttribute('aria-expanded', 'true');
        event.target.nextElementSibling.classList.toggle('show');
      }
    }
  }

  toggleExtraSubItem(event: any) {
    let isCurrentMenuId = event.target.closest('a.nav-link');
    let isMenu = isCurrentMenuId.nextElementSibling as any;
    if (isMenu.classList.contains('show')) {
      isMenu.classList.remove('show');
      isCurrentMenuId.setAttribute('aria-expanded', 'false');
    } else {
      let dropDowns = Array.from(document.querySelectorAll('.extra-sub-menu'));
      dropDowns.forEach((node: any) => {
        node.classList.remove('show');
      });

      let subDropDowns = Array.from(
        document.querySelectorAll('.menu-dropdown .nav-link')
      );
      subDropDowns.forEach((submenu: any) => {
        submenu.setAttribute('aria-expanded', 'false');
      });

      if (event.target && event.target.nextElementSibling) {
        isCurrentMenuId.setAttribute('aria-expanded', 'true');
        event.target.nextElementSibling.classList.toggle('show');
      }
    }
  }

  updateActive(event: any) {
    const ul = document.getElementById('navbar-nav');
    if (ul) {
      const items = Array.from(ul.querySelectorAll('a.nav-link.active'));
      this.removeActivation(items);
    }
    this.activateParentDropdown(event.target);
  }

  // Click wise Parent active class add
  toggleParentItem(event: any) {
    let isCurrentMenuId = event.target.getAttribute('subitems');
    let isMenu = document.getElementById(isCurrentMenuId) as any;
    let dropDowns = Array.from(document.querySelectorAll('#navbar-nav .show'));
    dropDowns.forEach((node: any) => {
      node.classList.remove('show');
    });
    isMenu ? isMenu.classList.add('show') : null;

    const ul = document.getElementById('two-column-menu');
    if (ul) {
      const iconItems = Array.from(ul.getElementsByTagName('a'));
      let activeIconItems = iconItems.filter((x: any) =>
        x.classList.contains('active')
      );
      activeIconItems.forEach((item: any) => {
        item.classList.remove('active');
      });
    }
    event.target.classList.add('active');
    document.body.classList.add('twocolumn-panel');
  }

  toggleItem(event: any) {
    // show navbar-nav menu on click of icon sidebar menu
    let isCurrentMenuId = event.target.getAttribute('subitems');
    let isMenu = document.getElementById(isCurrentMenuId) as any;
    let dropDowns = Array.from(document.querySelectorAll('#navbar-nav .show'));
    dropDowns.forEach((node: any) => {
      node.classList.remove('show');
    });
    isMenu ? isMenu.classList.add('show') : null;

    const ul = document.getElementById('two-column-menu');
    if (ul) {
      const iconItems = Array.from(ul.getElementsByTagName('a'));
      let activeIconItems = iconItems.filter((x: any) =>
        x.classList.contains('active')
      );
      activeIconItems.forEach((item: any) => {
        item.classList.remove('active');
      });
    }
    event.target.classList.add('active');
    document.body.classList.remove('twocolumn-panel');
  }

  // remove active items of two-column-menu
  removeActivation(items: any) {
    items.forEach((item: any) => {
      if (item.classList.contains('menu-link')) {
        if (!item.classList.contains('active')) {
          item.setAttribute('aria-expanded', false);
        }
        item.nextElementSibling.classList.remove('show');
      }
      if (item.classList.contains('nav-link')) {
        if (item.nextElementSibling) {
          item.nextElementSibling.classList.remove('show');
        }
        if (item.parentElement) {
          item.parentElement.closest('.collapse')?.classList.remove('show');
        }
        item.setAttribute('aria-expanded', false);
      }
      item.classList.remove('active');
    });
  }

  activateIconSidebarActive(id: any) {
    var menu = document.querySelector(
      "#two-column-menu .simplebar-content-wrapper a[subitems='" +
        id +
        "'].nav-icon"
    );
    if (menu !== null) {
      menu.classList.add('active');
    }
  }

  activateParentDropdown(item: any) {
    // navbar-nav menu add active
    item.classList.add('active');
    let parentCollapseDiv = item.closest('.collapse.menu-dropdown');
    if (parentCollapseDiv) {
      // to set aria expand true remaining
      parentCollapseDiv.classList.add('show');
      parentCollapseDiv.parentElement.children[0].classList.add('active');
      parentCollapseDiv.parentElement.children[0].setAttribute(
        'aria-expanded',
        'true'
      );
      if (parentCollapseDiv.parentElement.closest('.collapse.menu-dropdown')) {
        parentCollapseDiv.parentElement
          .closest('.collapse')
          .classList.add('show');
        if (
          parentCollapseDiv.parentElement.closest('.collapse')
            .previousElementSibling
        )
          parentCollapseDiv.parentElement
            .closest('.collapse')
            .previousElementSibling.classList.add('active');
      }
      this.activateIconSidebarActive(parentCollapseDiv.getAttribute('id'));
      return false;
    }
    return false;
  }

  initActiveMenu() {
    const pathName = window.location.pathname;

    // Active Main Single Menu
    const mainItems = Array.from(
      document.querySelectorAll('.twocolumn-iconview li a')
    );
    let matchingMainMenuItem = mainItems.find((x: any) => {
      if (x.classList.contains('active')) {
        x.classList.remove('active');
      }
      return x.pathname === pathName;
    });
    if (matchingMainMenuItem) {
      this.activateParentDropdown(matchingMainMenuItem);
    }

    // Active Sub Menu
    const ul = document.getElementById('navbar-nav');
    if (ul) {
      const items = Array.from(ul.querySelectorAll('a.nav-link'));
      let activeItems = items.filter((x: any) =>
        x.classList.contains('active')
      );
      this.removeActivation(activeItems);

      let matchingMenuItem = items.find((x: any) => {
        return x.pathname === pathName;
      });

      if (matchingMenuItem) {
        this.activateParentDropdown(matchingMenuItem);
      } else {
        var id = pathName.replace('/', '');
        if (id) document.body.classList.add('twocolumn-panel');
        this.activateIconSidebarActive(id);
      }
    }
  }

  /**
   * Returns true or false if given menu item has child or not
   * @param item menuItem
   */
  hasItems(item: MenuItem) {
    return item.subItems !== undefined ? item.subItems.length > 0 : false;
  }

  /**
   * SidebarHide modal
   * @param content modal content
   */
  SidebarHide() {
    document.body.classList.remove('vertical-sidebar-enable');
  }
}

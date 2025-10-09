import {
  Component,
  OnInit,
  OnDestroy,
  EventEmitter,
  Output,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { EventService } from '../../core/services/event.service';

import { MENU } from '././../../menu/menu';
import { MenuItem } from './../../menu/menu.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit, OnDestroy {
  menu: any;
  toggle: any = true;
  menuItems: MenuItem[] = [];
  filteredMenuItems: MenuItem[] = [];
  searchTerm: string = '';
  currentTheme: string = 'light';
  private themeSubscription: any;
  @ViewChild('sideMenu') sideMenu!: ElementRef;
  @Output() mobileMenuButtonClicked = new EventEmitter();

  constructor(
    private router: Router,
    public translate: TranslateService,
    private eventService: EventService
  ) {
    translate.setDefaultLang('tr');
  }

  ngOnInit(): void {
    // Initialize theme from current document attribute
    const currentTheme =
      document.documentElement.getAttribute('data-bs-theme') || 'light';
    this.currentTheme = currentTheme;

    // Listen for theme change events
    this.themeSubscription = this.eventService.subscribe(
      'changeMode',
      (mode: string) => {
        this.currentTheme = mode;
      }
    );

    // Menu Items
    this.menuItems = MENU;
    this.filteredMenuItems = [...this.menuItems];
    this.router.events.subscribe((event) => {
      if (document.documentElement.getAttribute('data-layout') != 'twocolumn') {
        if (event instanceof NavigationEnd) {
          this.initActiveMenu();
        }
      }
    });
  }

  ngOnDestroy(): void {
    if (this.themeSubscription) {
      this.themeSubscription.unsubscribe();
    }
  }

  /***
   * Activate droup down set
   */
  ngAfterViewInit() {
    setTimeout(() => {
      this.initActiveMenu();
    }, 0);
  }

  removeActivation(items: any) {
    items.forEach((item: any) => {
      if (item.classList.contains('menu-link')) {
        if (!item.classList.contains('active')) {
          item.setAttribute('aria-expanded', false);
        }
        item.nextElementSibling
          ? item.nextElementSibling.classList.remove('show')
          : null;
      }
      if (item.classList.contains('nav-link')) {
        if (item.nextElementSibling) {
          item.nextElementSibling.classList.remove('show');
        }
        item.setAttribute('aria-expanded', false);
      }
      item.classList.remove('active');
    });
  }

  toggleSubItem(event: any) {
    let isCurrentMenuId = event.target.closest('a.nav-link');
    let isMenu = isCurrentMenuId.nextElementSibling as any;
    if (isMenu.classList.contains('show')) {
      isMenu.classList.remove('show');
      isCurrentMenuId.setAttribute('aria-expanded', 'false');
    } else {
      let dropDowns = Array.from(document.querySelectorAll('.sub-menu'));
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

  // Click wise Parent active class add
  toggleParentItem(event: any) {
    let isCurrentMenuId = event.target.closest('a.nav-link');
    let dropDowns = Array.from(document.querySelectorAll('#navbar-nav .show'));
    dropDowns.forEach((node: any) => {
      node.classList.remove('show');
    });
    const ul = document.getElementById('navbar-nav');
    if (ul) {
      const iconItems = Array.from(ul.getElementsByTagName('a'));
      let activeIconItems = iconItems.filter((x: any) =>
        x.classList.contains('active')
      );
      activeIconItems.forEach((item: any) => {
        item.setAttribute('aria-expanded', 'false');
        item.classList.remove('active');
      });
    }
    isCurrentMenuId.setAttribute('aria-expanded', 'true');
    if (isCurrentMenuId) {
      this.activateParentDropdown(isCurrentMenuId);
    }
  }

  toggleItem(event: any) {
    let isCurrentMenuId = event.target.closest('a.nav-link');
    let isMenu = isCurrentMenuId.nextElementSibling as any;
    if (isMenu.classList.contains('show')) {
      isMenu.classList.remove('show');
      isCurrentMenuId.setAttribute('aria-expanded', 'false');
    } else {
      let dropDowns = Array.from(
        document.querySelectorAll('#navbar-nav .show')
      );
      dropDowns.forEach((node: any) => {
        node.classList.remove('show');
      });
      isMenu ? isMenu.classList.add('show') : null;
      const ul = document.getElementById('navbar-nav');
      if (ul) {
        const iconItems = Array.from(ul.getElementsByTagName('a'));
        let activeIconItems = iconItems.filter((x: any) =>
          x.classList.contains('active')
        );
        activeIconItems.forEach((item: any) => {
          item.setAttribute('aria-expanded', 'false');
          item.classList.remove('active');
        });
      }
      isCurrentMenuId.setAttribute('aria-expanded', 'true');
      if (isCurrentMenuId) {
        this.activateParentDropdown(isCurrentMenuId);
      }
    }
  }

  activateParentDropdown(item: any) {
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
        if (
          parentCollapseDiv.parentElement
            .closest('.collapse')
            .previousElementSibling.closest('.collapse')
        ) {
          parentCollapseDiv.parentElement
            .closest('.collapse')
            .previousElementSibling.closest('.collapse')
            .classList.add('show');
          parentCollapseDiv.parentElement
            .closest('.collapse')
            .previousElementSibling.closest('.collapse')
            .previousElementSibling.classList.add('active');
        }
      }
      return false;
    }
    return false;
  }

  updateActive(event: any) {
    const ul = document.getElementById('navbar-nav');
    if (ul) {
      const items = Array.from(ul.querySelectorAll('a.nav-link'));
      this.removeActivation(items);
    }
    this.activateParentDropdown(event.target);
  }

  initActiveMenu() {
    const pathName = window.location.pathname;
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
   * Get logo path based on current theme
   */
  getLogoPath(): string {
    return this.currentTheme === 'dark'
      ? 'assets/images/logo-console-log-dark.png'
      : 'assets/images/logo-console-log-light.png';
  }

  /**
   * Search menu items
   */
  onSearchMenu(searchTerm: string) {
    this.searchTerm = searchTerm;

    if (!this.searchTerm.trim()) {
      this.filteredMenuItems = [...this.menuItems];
      return;
    }

    const normalizedSearchTerm = this.normalizeText(this.searchTerm);
    this.filteredMenuItems = this.filterMenuItems(
      this.menuItems,
      normalizedSearchTerm
    );
  }

  /**
   * Normalize text for Turkish character support and case insensitive search
   */
  private normalizeText(text: string): string {
    if (!text) return '';

    // First convert to lowercase for case insensitive search
    let normalized = text.toLowerCase();

    // Turkish character mappings
    const turkishCharMap: { [key: string]: string } = {
      ğ: 'g',
      Ğ: 'g',
      ü: 'u',
      Ü: 'u',
      ş: 's',
      Ş: 's',
      ı: 'i',
      İ: 'i',
      î: 'i',
      í: 'i',
      ì: 'i',
      ö: 'o',
      Ö: 'o',
      ô: 'o',
      ó: 'o',
      ò: 'o',
      ç: 'c',
      Ç: 'c',
      â: 'a',
      á: 'a',
      à: 'a',
      ã: 'a',
      ê: 'e',
      é: 'e',
      è: 'e',
      û: 'u',
      ú: 'u',
      ù: 'u',
    };

    // Replace Turkish and other accented characters
    for (const [accented, plain] of Object.entries(turkishCharMap)) {
      normalized = normalized.replace(new RegExp(accented, 'g'), plain);
    }

    return normalized.trim();
  }

  /**
   * Filter menu items recursively
   */
  private filterMenuItems(items: MenuItem[], searchTerm: string): MenuItem[] {
    const filtered: MenuItem[] = [];

    items.forEach((item) => {
      if (item.isTitle) {
        // Keep title items as they are
        filtered.push(item);
        return;
      }

      const itemMatches = this.itemMatches(item, searchTerm);
      const hasMatchingSubItems =
        item.subItems && this.hasMatchingSubItems(item.subItems, searchTerm);

      if (itemMatches || hasMatchingSubItems) {
        const filteredItem: MenuItem = { ...item };

        if (hasMatchingSubItems && item.subItems) {
          filteredItem.subItems = this.filterMenuItems(
            item.subItems,
            searchTerm
          );
        }

        filtered.push(filteredItem);
      }
    });

    return filtered;
  }

  /**
   * Check if menu item matches search term
   */
  private itemMatches(item: MenuItem, searchTerm: string): boolean {
    if (!item.label) return false;

    // Check both the translation key and the translated text
    const normalizedLabel = this.normalizeText(item.label);
    const translatedText = this.translate.instant(item.label);
    const normalizedTranslatedText = this.normalizeText(translatedText);

    // Also check if translated text is different from key (meaning translation exists)
    const hasTranslation = translatedText && translatedText !== item.label;

    const keyMatches = normalizedLabel.includes(searchTerm);
    const translationMatches =
      hasTranslation && normalizedTranslatedText.includes(searchTerm);

    return keyMatches || translationMatches;
  }

  /**
   * Check if any sub items match search term
   */
  private hasMatchingSubItems(
    subItems: MenuItem[],
    searchTerm: string
  ): boolean {
    return subItems.some(
      (subItem) =>
        this.itemMatches(subItem, searchTerm) ||
        (subItem.subItems &&
          this.hasMatchingSubItems(subItem.subItems, searchTerm))
    );
  }

  /**
   * Clear search
   */
  clearSearch() {
    this.searchTerm = '';
    this.filteredMenuItems = [...this.menuItems];
  }

  /**
   * Toggle the menu bar when having mobile screen
   */
  toggleMobileMenu(event: any) {
    var sidebarsize =
      document.documentElement.getAttribute('data-sidebar-size');
    if (sidebarsize == 'sm-hover-active') {
      document.documentElement.setAttribute('data-sidebar-size', 'sm-hover');
    } else {
      document.documentElement.setAttribute(
        'data-sidebar-size',
        'sm-hover-active'
      );
    }
  }

  /**
   * SidebarHide modal
   * @param content modal content
   */
  SidebarHide() {
    document.body.classList.remove('vertical-sidebar-enable');
  }
}

import { LayoutService } from './../../core/services/layout.service';
import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { EventService } from '../../core/services/event.service';
import {
  LAYOUT_VERTICAL,
  LAYOUT_MODE,
  LAYOUT_WIDTH,
  LAYOUT_POSITION,
  TOPBAR,
  SIDEBAR_SIZE,
  SIDEBAR_VIEW,
  SIDEBAR_COLOR,
  BODY_IMAGE,
  DATA_PRELOADER,
  SIDEBAR_VISIBILITY,
} from '../layout.model';
import { NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import themes from 'devextreme/ui/themes';

@Component({
  selector: 'app-rightsidebar',
  templateUrl: './rightsidebar.component.html',
  styleUrls: ['./rightsidebar.component.scss'],
})

/**
 * Right Sidebar component
 */
export class RightsidebarComponent implements OnInit {
  layout: string | undefined;
  mode: string | undefined;
  width: string | undefined;
  position: string | undefined;
  topbar: string | undefined;
  size: string | undefined;
  sidebarView: string | undefined;
  sidebar: string | undefined;
  attribute: any;
  preLoader: any;
  sidebarVisibility: any;
  bodyImage: any;
  grd: any;

  @Output() settingsButtonClicked = new EventEmitter();
  @ViewChild('filtetcontent') filtetcontent!: TemplateRef<any>;
  constructor(
    private eventService: EventService,
    private offcanvasService: NgbOffcanvas,
    private layoutService: LayoutService
  ) {}

  ngOnInit(): void {
    setTimeout(() => {
      if (this.offcanvasService.hasOpenOffcanvas() == false) {
        this.openEnd(this.filtetcontent);
      }
    }, 1000);
    var layoutSettings = this.layoutService.getLayoutSettings();

    this.layout = layoutSettings.layoutType;
    this.mode = layoutSettings.layoutMode;
    this.width = layoutSettings.layoutWidth;
    this.position = layoutSettings.layoutPosition;
    this.topbar = layoutSettings.topBar;
    this.size = layoutSettings.sideBarSize;
    this.sidebarView = layoutSettings.sideBarView;
    this.sidebar = layoutSettings.sideBarColor;
    this.preLoader = layoutSettings.dataPreLoader;
    this.bodyImage = layoutSettings.bodyImage;
    this.sidebarVisibility = layoutSettings.sideBarVisibility;
    this.attribute = '';

    this.eventService.subscribe('changeMode', (mode: string) => {
      this.mode = mode;
    });
  }

  ngAfterViewInit() {}

  /**
   * Change the layout onclick
   * @param layout Change the layout
   */
  changeLayout(layout: string) {
    this.attribute = layout;
    this.layout = layout;

    if (layout == 'semibox') {
      this.eventService.broadcast('changeLayout', 'vertical');
    } else {
      this.eventService.broadcast('changeLayout', layout);
    }

    document.documentElement.setAttribute('data-layout', layout);

    this.layoutService.setLayoutSettings(
      layout,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );

    setTimeout(() => {
      window.dispatchEvent(new Event('resize'));
    }, 1000);
  }

  setLayout(layout: string) {
    this.attribute = layout;
    this.sidebar = 'light';
    this.layout = layout;
    document.documentElement.setAttribute('data-layout', layout);

    this.layoutService.setLayoutSettings(
      layout,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Add Active Class
  addActive(grdSidebar: any) {
    this.grd = grdSidebar;
    document.documentElement.setAttribute('data-sidebar', grdSidebar);
    document.getElementById('collapseBgGradient')?.classList.toggle('show');
    document.getElementById('collapseBgGradient1')?.classList.add('active');
  }

  // Remove Active Class
  removeActive() {
    this.grd = '';
    document.getElementById('collapseBgGradient1')?.classList.remove('active');
    document.getElementById('collapseBgGradient')?.classList.remove('show');
  }

  // When the user clicks on the button, scroll to the top of the document
  topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
  }

  //  Filter Offcanvas Set
  openEnd(content: TemplateRef<any>) {
    this.offcanvasService.open(content, { position: 'end' });
    setTimeout(() => {
      this.attribute = document.documentElement.getAttribute('data-layout');
      if (this.attribute == 'vertical') {
        var vertical = document.getElementById(
          'customizer-layout01'
        ) as HTMLInputElement;
        if (vertical != null) {
          vertical.setAttribute('checked', 'true');
        }
      }
      if (this.attribute == 'horizontal') {
        const horizontal = document.getElementById('customizer-layout02');
        if (horizontal != null) {
          horizontal.setAttribute('checked', 'true');
        }
      }
      if (this.attribute == 'twocolumn') {
        const Twocolumn = document.getElementById('customizer-layout03');
        if (Twocolumn != null) {
          Twocolumn.setAttribute('checked', 'true');
        }
      }
      if (this.attribute == 'semibox') {
        const Twocolumn = document.getElementById('customizer-layout04');
        if (Twocolumn != null) {
          Twocolumn.setAttribute('checked', 'true');
        }
      }
    }, 100);
  }

  // Mode Change
  changeMode(mode: string) {
    this.mode = mode;
    document.documentElement.setAttribute('data-layout-mode', mode);

    this.layoutService.setLayoutSettings(
      this.layout!,
      mode,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );

    switch (mode) {
      case 'light':
        window.localStorage.setItem('dx-theme', 'light');
        themes.current('light');
        break;
      case 'dark':
        window.localStorage.setItem('dx-theme', 'dark');
        themes.current('dark');
        break;
      default:
        window.localStorage.setItem('dx-theme', 'light');
        themes.current('light');
        break;
    }
  }

  // Visibility Change
  changeVisibility(visibility: string) {
    this.sidebarVisibility = visibility;
    document.documentElement.setAttribute(
      'data-sidebar-visibility',
      visibility
    );

    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      visibility,
      this.bodyImage
    );
  }

  // Width Change
  changeWidth(width: string, size: string) {
    this.width = width;
    document.documentElement.setAttribute('data-layout-width', width);
    document.documentElement.setAttribute('data-sidebar-size', size);

    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      width,
      this.position!,
      this.topbar!,
      size,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Position Change
  changePosition(position: string) {
    this.position = position;
    document.documentElement.setAttribute('data-layout-position', position);
    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      position,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Topbar Change
  changeTopColor(color: string) {
    this.topbar = color;
    document.documentElement.setAttribute('data-topbar', color);

    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      color,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Sidebar Size Change
  changeSidebarSize(size: string) {
    this.size = size;
    document.documentElement.setAttribute('data-sidebar-size', size);

    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      size,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Sidebar Size Change
  changeSidebar(sidebar: string) {
    this.sidebarView = sidebar;
    document.documentElement.setAttribute('data-layout-style', sidebar);
    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      sidebar,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Sidebar Color Change
  changeSidebarColor(color: string) {
    this.sidebar = color;
    document.documentElement.setAttribute('data-sidebar', color);
    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      color,
      this.preLoader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // PreLoader Image Change
  changeLoader(loader: string) {
    this.preLoader = loader;
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

    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      loader,
      this.sidebarVisibility,
      this.bodyImage
    );
  }

  // Body Image Change
  changebodyImage(img: string) {
    this.bodyImage = img;
    document.documentElement.setAttribute('data-body-image', img);
    this.layoutService.setLayoutSettings(
      this.layout!,
      this.mode!,
      this.width!,
      this.position!,
      this.topbar!,
      this.size!,
      this.sidebarView!,
      this.sidebar!,
      this.preLoader,
      this.sidebarVisibility,
      img
    );
  }
}

# BOnlineStore UI - Copilot Instructions

## Project Overview

**Velzon** is an Angular 16 + DevExtreme enterprise business application for managing definitions, production orders, and settings. It uses modular architecture with lazy-loaded feature modules (Definitions, Production, Settings) behind a unified layout system with role-based auth (OIDC).

### Tech Stack

- **Framework**: Angular 16.2.12 with custom webpack builder
- **UI Grid**: DevExtreme 23.2.11 for data-bound tables
- **Styling**: SCSS with Bootstrap 5.3.2
- **Auth**: OIDC Client (oidc-client-ts) + JWT interceptors
- **Data**: REST APIs with custom HTTP interceptors
- **i18n**: @ngx-translate (default: Turkish "tr")

---

## Critical Architecture Patterns

### 1. **Modular Feature Architecture with Lazy Loading**

Routes are defined in `app-routing.module.ts` with lazy-loaded modules:

```typescript
// src/app/app-routing.module.ts
{
  path: 'definitions',
  component: LayoutComponent,
  loadChildren: () => import('./definitions/definitions.module').then(m => m.DefinitionsModule)
}
```

**Key modules:**

- `/auth` → Account (login/register)
- `/` → Pages (dashboard)
- `/definitions` → Master data (Bank, Currency, Material, etc.)
- `/production` → Work orders, formulas
- `/settings` → User configuration

### 2. **Base Service + DevExtreme DataSource Pattern**

Services inherit from `BaseService` (`src/app/base-classes/base-services/base-service.ts`):

- Wraps DevExtreme `DataSource` and `CustomStore` with CRUD operations
- Handles HTTP POST/PUT/DELETE via `sendRequest()` method
- Converts Observable→Promise with `lastValueFrom()`

**Example:** `BankService` extends `BaseService` and returns `DataSource` for bound grids:

```typescript
export class BankService extends BaseService {
  constructor(public override _http: HttpClient) {
    super(_http, environment.definitionsUrl, DefinitionsControllerNamesEnum.BANK);
  }
  getDataSource(): DataSource {
    return super.getBaseDataSource(); // Pre-configured with CRUD endpoints
  }
}
```

### 3. **Base Component for Definition Lists**

`BaseDefinitionsOnGridComponent` (`src/app/base-classes/base-definitions-on-grid/`) provides:

- Auto-generated breadcrumbs (translation keys from i18n)
- Excel/PDF export via `onExporting()`
- DataGrid refresh & unsubscribe lifecycle

**Pattern:** Definition components (Bank, Currency, etc.) extend this class and pass DataSource:

```typescript
export class BankComponent extends BaseDefinitionsOnGridComponent {
  public bankDataSource: DataSource;
  constructor(public override _translate: TranslateService, private _bankService: BankService) {
    super(_translate, "BANK", "BANK", "DEFINITIONS"); // file name, component key, parent key
    this.bankDataSource = _bankService.getDataSource();
  }
}
```

### 4. **OIDC Authentication + JWT Interceptors**

- **AuthenticationService** (`src/app/core/services/auth.service.ts`): Manages OIDC user manager, token persistence in `sessionStorage`
- **JwtInterceptor** → Auto-injects Bearer token + language header on all HTTP requests
- **ErrorInterceptor** → Global error handling
- **AuthGuard** → Route protection; redirects unauthenticated users to OIDC login endpoint

**Configuration in environment files:**

```typescript
// src/environments/environment.ts
identityUrl: 'https://localhost:5001',
definitionsUrl: 'https://localhost:5011/api/',
productionUrl: 'https://localhost:5012/api/',
uiUrl: 'http://localhost:4200'
```

### 5. **Enumeration-Driven Configuration**

Key controller/API routing via enums in `src/app/base-classes/base-enums/`:

- `DefinitionsControllerNamesEnum` → Maps service names to API endpoints
- `HttpRequestMethodsEnum` → LOAD, LOADPOST, INSERT, UPDATE, DELETE
- `DatasourceFunctionsEnum` → Conditional DataSource configuration
- `AuthenticationScopesEnum` → OIDC scopes for different features

---

## Key File Locations & Patterns

| Purpose           | Location                      | Pattern                                                                       |
| ----------------- | ----------------------------- | ----------------------------------------------------------------------------- |
| API endpoints     | `src/app/global-component.ts` | Centralized `GlobalComponent.API_URL`                                         |
| Service layer     | `src/app/core/services/`      | @Injectable({providedIn:'root'}) singletons                                   |
| Models/DTOs       | `src/app/dtos/`               | Organized by feature (definitions, production, settings)                      |
| Interceptors      | `src/app/core/helpers/`       | Implement HttpInterceptor with multi: true                                    |
| Guards            | `src/app/core/guards/`        | AuthGuard checks `currentUser()` before route activation                      |
| Layouts           | `src/app/layouts/`            | LayoutComponent wraps feature modules; supports vertical/horizontal/twocolumn |
| i18n translations | `src/assets/i18n/`            | JSON files per language (tr, en); loaded via TranslateLoader                  |

---

## Development Workflows

### Build & Serve

```powershell
# Development with HMR (hot module replacement)
npm run start-hmr

# Standard dev server (auto-reload on file changes)
npm start

# Production build with optimization
npm run build-prod

# Analyze bundle size
npm run build-stats
npm run bundle-analyzer
```

**Memory notes:** `NODE_OPTIONS=--max_old_space_size=8192` set in dev; use 3072 for production builds.

### Testing

```powershell
ng test           # Unit tests via Karma + Jasmine
ng lint           # TypeScript linting
```

### Local OIDC Setup

Update `src/environments/environment.ts` to match your Identity Server:

- `identityUrl` → OIDC Authority (e.g., IdentityServer4 endpoint)
- `definitionsUrl` / `productionUrl` → Microservice API base URLs
- Ensure CORS headers allow localhost:4200

---

## Code Conventions

1. **Component Selector Naming**: Use kebab-case (e.g., `selector: 'bank'`, not `bankComponent`)
2. **Translated UI Text**: Always use `{{ 'KEY' | translate }}` in templates; never hardcode strings
3. **Service Requests**: Use `lastValueFrom()` to convert Observable→Promise; no manual subscriptions in services
4. **Token Management**: JWT stored in `currentUser()` BehaviorSubject; cleared on logout via OIDC
5. **DataGrid Export**: Inherit from `BaseDefinitionsOnGridComponent` to auto-get Excel/PDF export buttons
6. **Error Handling**: ErrorInterceptor catches HTTP errors; display via toast/alert service
7. **Breadcrumbs**: Automatically generated from i18n keys passed to base component constructor

---

## Common Tasks

### Add a New Definition Page

1. Create folder: `src/app/definitions/[feature-name]/`
2. Generate service extending `BaseService` with controller enum
3. Create component extending `BaseDefinitionsOnGridComponent`
4. Add to `definitions.module.ts` imports
5. Add route to `definitions-routing.module.ts`

### Add an API Endpoint

1. Extend service method in appropriate `*.service.ts`
2. Use `BaseService.sendRequest()` or `httpPostRequest()`
3. Intercept auth headers automatically via `JwtInterceptor`

### Add Translation

1. Add key-value pair to `src/assets/i18n/[lang].json`
2. Use in template: `{{ 'KEY' | translate }}`
3. In code: `this._translate.get('KEY').subscribe(t => ...)`

---

## Troubleshooting

- **"Failed to compile"**: Increase Node memory: `set NODE_OPTIONS=--max_old_space_size=8192`
- **OIDC redirect loop**: Check `identityUrl` in environment matches Identity Server configuration
- **DataGrid not binding**: Ensure service returns `DataSource` from `getBaseDataSource()`
- **Translations missing**: Verify i18n JSON files exist and `TranslateLoader` is configured in `AppModule`
- **Token expiration**: OIDC auto-renewal handled by `UserManager.automaticSilentRenew = true`

---

## Production Module Patterns vs Definitions

### **Definitions Module** (Simple CRUD)

- **Purpose**: Master data entities (Bank, Currency, Material, Color, etc.)
- **Service Pattern**: Basic `BaseService.getBaseDataSource()` with standard CRUD
- **Editing Mode**: `batch` mode (inline editing per row)
- **DataSources**: Single primary grid + optional lookup dropdowns
- **Example**: `src/app/definitions/bank/` with 3 files (component, service, template)

### **Production Module** (Complex Business Logic)

- **Purpose**: Work orders, formulas, production calculations
- **Service Pattern**: Extends `BaseService` + custom business logic methods (e.g., `calculateProductionList`)
- **Editing Mode**: `popup` mode (modal form with form groups/validation)
- **DataSources**: Primary grid + multiple linked CustomStore dropdowns for lookups
- **Cross-Service Calls**: References both `productionUrl` and `definitionsUrl` endpoints

**Key Pattern Difference:**

```typescript
// Production Service: Multiple lookup data sources from DIFFERENT microservices
export class WorkOrderService extends BaseService {
  getRawModelDataSource(): CustomStore {
    return super.getBaseRawCustomStore(
      environment.definitionsUrl, // DIFFERENT URL
      DefinitionsControllerNamesEnum.MODEL // Different controller
    );
  }

  calculateProductionList(workOrderId: string): Promise<WorkOrderFormFrontEndDto> {
    return this.httpGetRequestGeneric<WorkOrderFormFrontEndDto>(
      "CalculateProductionList",
      new HttpParams().append("workOrderId", workOrderId),
      BffControllerNamesEnum.ONLINESTORE,
      environment.bffUrl // Backend-for-Frontend microservice
    );
  }
}
```

**Template Differences:**

- **Definitions**: Simple columns with batch editing
- **Production**: Grouped form sections, nested groups, custom editors (`dxNumberBox`, `dxTextArea`), dynamic field visibility

---

## Microservice Integration Details

### **Service Architecture**

Three API endpoints are orchestrated via environment configuration:

| Service                        | URL Env          | Purpose                                   | Example Controllers                       |
| ------------------------------ | ---------------- | ----------------------------------------- | ----------------------------------------- |
| **Definitions**                | `definitionsUrl` | Master data (Bank, Material, Color, etc.) | BANK, COLOR, MATERIAL, MODEL              |
| **Production**                 | `productionUrl`  | Work orders, formulas                     | WORKORDER, FORMULA, FORMULATYPE           |
| **Backend-for-Frontend (BFF)** | `bffUrl`         | Computed/aggregated data                  | ONLINESTORE (calculates production lists) |

### **Cross-Service Data Binding**

**Scenario**: Work order form needs dropdown options from Definitions while submitting to Production:

```typescript
// WorkOrderComponent loads data from MULTIPLE sources
constructor(private _workOrderService: WorkOrderService) {
  // Grid data from Production API
  this.workOrderDataSource = _workOrderService.getDataSource(); // productionUrl/WorkOrder/Load

  // Dropdown options from Definitions API
  this.modelDataSource = _workOrderService.getRawModelDataSource(); // definitionsUrl/Model/LoadForCombo
  this.colorDataSource = _workOrderService.getRawColorDataSource(); // definitionsUrl/Color/Load
  this.firmDataSource = _workOrderService.getRawFirmDataSource();   // definitionsUrl/Firm/LoadForCombo

  // Computed data from BFF
  this.workOrderFormDto = await _workOrderService.calculateProductionListBff(workOrderId);
}
```

### **HTTP Header Injection**

`JwtInterceptor` automatically appends:

- Bearer token from OIDC `currentUser().token`
- Accept-Language header (e.g., `tr-TR` or `en-US`) based on user preference
- CORS headers

All requests go through this interceptor transparently.

---

## DevExtreme Grid Customization Examples

### **1. Batch Editing (Definitions Pattern)**

```html
<!-- src/app/definitions/bank/bank.component.html -->
<dx-data-grid [dataSource]="bankDataSource" ...>
  <dxo-editing mode="batch" <!-- Inline row editing --> [allowAdding]="true" [allowDeleting]="true" [allowUpdating]="true" [confirmDelete]="true" > </dxo-editing>

  <dxi-column dataField="code" caption="{{ 'CODE' | translate }}">
    <dxi-validation-rule type="required"></dxi-validation-rule>
    <dxi-validation-rule type="stringLength" [max]="50"></dxi-validation-rule>
  </dxi-column>
  <dxi-column dataField="name" caption="{{ 'NAME' | translate }}">
    <dxi-validation-rule type="required"></dxi-validation-rule>
  </dxi-column>
</dx-data-grid>
```

### **2. Popup Form Editing (Production Pattern)**

```html
<!-- src/app/production/work-order/work-order.component.html -->
<dx-data-grid [dataSource]="workOrderDataSource" ...>
  <dxo-editing mode="popup" <!-- Modal form editing -->
    [allowAdding]="true" [allowUpdating]="true" [useIcons]="true" [refreshMode]="'full'" >
    <dxo-form labelMode="floating">
      <!-- Grouped sections -->
      <dxi-item itemType="group" [colCount]="2" [colSpan]="2">
        <dxi-item itemType="group" caption="{{ 'WORKORDERINFORMATION' | translate }}">
          <dxi-item itemType="group" [colCount]="4">
            <dxi-item dataField="workOrderNo"></dxi-item>
            <dxi-item dataField="modelId" [colSpan]="3"></dxi-item>
          </dxi-item>

          <!-- Custom number format -->
          <dxi-item dataField="amount" editorType="dxNumberBox" [editorOptions]="{ format: '#,##0.00' }"></dxi-item>

          <!-- Multi-line text -->
          <dxi-item dataField="description" editorType="dxTextArea" [editorOptions]="{ height: 60 }" [colSpan]="2"></dxi-item>
        </dxi-item>
      </dxi-item>
    </dxo-form>
  </dxo-editing>
</dx-data-grid>
```

### **3. Lookup Columns with CustomStore**

```html
<!-- Dropdown populated from external DataSource -->
<dxi-column dataField="modelId" caption="{{ 'MODEL' | translate }}">
  <dxo-lookup [dataSource]="modelDataSource" displayExpr="name" valueExpr="id"> </dxo-lookup>
</dxi-column>

<dxi-column dataField="colorId" caption="{{ 'COLOR' | translate }}">
  <dxo-lookup [dataSource]="colorDataSource" displayExpr="name" valueExpr="id"> </dxo-lookup>
</dxi-column>
```

### **4. Export Configuration**

```html
<!-- Export to Excel/PDF automatically configured via BaseDefinitionsOnGridComponent -->
<dxo-export [enabled]="true" [formats]="['xlsx', 'pdf']"></dxo-export>

<!-- Component method handles file generation -->
onExporting(e: any) { e.fileName = this.fileName; // Set from i18n translation if (e.format === 'xlsx') { // ExcelJS workbook generation } else if (e.format === 'pdf') { // jsPDF generation } }
```

### **5. Column Chooser & Filtering**

```html
<dxo-column-chooser [enabled]="true" [mode]="'select'">
  <dxo-search [enabled]="true"></dxo-search>
</dxo-column-chooser>

<dxo-filter-row [visible]="true"></dxo-filter-row>
<dxo-header-filter [visible]="true"></dxo-header-filter>
<dxo-filter-panel [visible]="true"></dxo-filter-panel>
<dxo-filter-builder [allowHierarchicalFields]="true"></dxo-filter-builder>

<dxo-toolbar>
  <dxi-item name="groupPanel"></dxi-item>
  <dxi-item name="addRowButton"></dxi-item>
  <dxi-item name="saveButton"></dxi-item>
  <dxi-item name="revertButton"></dxi-item>
  <dxi-item name="exportButton"></dxi-item>
  <dxi-item name="columnChooserButton"></dxi-item>
</dxo-toolbar>
```

---

## CI/CD & Deployment Considerations

### **Docker Build Process**

See `Dockerfile` - Two-stage build optimized for size & performance:

**Stage 1 - Build:**

- Uses `node:18.20.5`
- Sets `NODE_OPTIONS=--max_old_space_size=3072` for production builds
- Runs `npm run build-prod` with aggressive optimization flags
- Output: `/dist/velzon` directory

**Stage 2 - Runtime:**

- Uses lightweight `nginx:latest`
- Serves static Angular assets from `/usr/share/nginx/html`
- SSL certificates mounted at `/etc/nginx/certs/`
- Exposes ports 80 (HTTP) and 443 (HTTPS)

```dockerfile
FROM node:18.20.5 AS build
ENV NODE_OPTIONS="--max_old_space_size=3072"
RUN npm run build-prod -- --optimization=true --build-optimizer=true --aot=true --source-map=false

FROM nginx:latest
COPY --from=build /dist/src/app/dist/velzon /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY ssl/bonlinestore.crt /etc/nginx/certs/bonlinestore.crt
COPY ssl/bonlinestore-decrypted.key /etc/nginx/certs/bonlinestore.key
EXPOSE 80 443
```

### **Nginx Configuration** (`nginx.conf`)

- **SSL Certificate Handling**: Uses pre-mounted PEM certificates (production-grade)
- **SPA Routing**: `try_files $uri $uri/ /index.html =404` → all routes fallback to index.html
- **Logging**: Detailed request/response logging with request body + timing metrics

```nginx
server {
  listen 443 ssl;
  ssl_certificate /etc/nginx/certs/bonlinestore.crt;
  ssl_certificate_key /etc/nginx/certs/bonlinestore.key;

  location / {
    try_files $uri $uri/ /index.html =404;
  }
}
```

### **Environment-Specific Build Strategies**

| Target                | Command                                          | Settings                                                     |
| --------------------- | ------------------------------------------------ | ------------------------------------------------------------ |
| **Development**       | `npm start`                                      | HMR enabled, source maps, loose optimization                 |
| **Staging/Perf Test** | `npm run start-hmr`                              | HMR + source maps for debugging performance                  |
| **Production**        | `npm run build-prod`                             | Full optimization, AOT, no source maps, bundled              |
| **Bundle Analysis**   | `npm run build-stats && npm run bundle-analyzer` | Generates `dist/velzon/stats.json` for webpack visualization |

### **OIDC Endpoint Configuration**

Deployment MUST update `src/environments/environment.prod.ts`:

```typescript
export const environment = {
  production: true,
  identityUrl: "https://your-identity-server.com", // OIDC Authority
  definitionsUrl: "https://your-api.com/definitions/api/",
  productionUrl: "https://your-api.com/production/api/",
  bffUrl: "https://your-api.com/bff/api/",
  uiUrl: "https://your-ui-domain.com", // Callback redirect URI
};
```

### **Container Orchestration Notes**

- **Memory**: Set `NODE_OPTIONS` to 3072MB for production builds (container memory budget)
- **SSL Certs**: Mount as volumes or secret in K8s; never commit to repo
- **API Proxying**: If services behind reverse proxy, configure at infrastructure level (nginx/Kong)
- **CORS**: Ensure `uiUrl` is whitelisted in API CORS configuration

---

## Key Dependencies & Integrations

- **DevExtreme**: Data grids, forms (load/insert/update/remove ops via DataSource)
- **Bootstrap**: Layout & utility classes (flex, grid, spacing)
- **RxJS**: Observables for async data & events
- **Firebase/OIDC**: Pluggable auth backends (currently OIDC)
- **@bonlinestore/schematics**: Custom Angular schematics for scaffolding

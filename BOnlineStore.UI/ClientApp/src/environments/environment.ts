// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  identityUrl: 'https://localhost:5001',
  //identityUrl: 'https://localhost:5000/identity',
  //definitionsUrl: 'https://localhost:5000/services/definitions/',
  definitionsUrl: 'https://localhost:5011/api/',
  //productionUrl: 'https://localhost:5000/services/production/',
  productionUrl: 'https://localhost:5012/api/',
  bffUrl: 'https://localhost:5013/api/',
  uiUrl: 'http://localhost:4200',
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.

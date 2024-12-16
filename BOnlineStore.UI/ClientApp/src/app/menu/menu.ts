import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [
  {
    id: 10010,
    label: 'DASHBOARD',
    icon: 'ri-dashboard-2-line',
  },
  {
    id: 10020,
    label: 'DEFINITIONS',
    icon: 'ri-apps-2-line',
    collapseid: 'sidebarDefinitions',
    subItems: [
      {
        id: 10030,
        label: 'MODELDEFINITIONSTITLE',
        parentId: 10020,
        subItems: [
          {
            id: 10040,
            label: 'MODEL',
            link: '/definitions/model',
            parentId: 10030,
          },
          {
            id: 10050,
            label: 'PANEL',
            link: '/definitions/panel',
            parentId: 10030,
          },
          {
            id: 10060,
            label: 'MODELGROUPS',
            link: '/definitions/model-groups',
            parentId: 10030,
            badge: {
              variant: 'bg-success',
              text: 'NEW',
            },
          },
        ],
      },
      {
        id: 10070,
        label: 'RAWMATERIALDEFINITIONSTITLE',
        parentId: 10020,
        subItems: [
          {
            id: 10080,
            label: 'RAWMATERIAL',
            link: '/definitions/raw-material',
            parentId: 10070,
          },
          {
            id: 10090,
            label: 'RAWMATERIALGROUPS',
            link: '/definitions/raw-material-groups',
            parentId: 10070,
          },
        ],
      },
      {
        id: 10100,
        label: 'ASSEMBLYDEFINITIONSTITLE',
        parentId: 10020,
        subItems: [
          {
            id: 10110,
            label: 'MEASUREMENTASSEMBLYLIMITS',
            link: '/definitions/measurement-assembly-limits',
            parentId: 10100,
          },
          {
            id: 10115,
            label: 'ASSEMBLYPRICE',
            link: '/definitions/assembly-price',
            parentId: 10100,
          },
          {
            id: 10120,
            label: 'ASSEMBLER',
            link: '/definitions/assembler',
            parentId: 10100,
          },
        ],
      },
      {
        id: 10130,
        label: 'PRODUCTIONDEFINITIONSTITLE',
        parentId: 10020,
        subItems: [
          {
            id: 10135,
            label: 'RECIPETYPE',
            link: '/definitions/recipe-type',
            parentId: 10130,
          },
          {
            id: 10140,
            label: 'GLASS',
            link: '/definitions/glass',
            parentId: 10130,
          },
          {
            id: 10150,
            label: 'GLASSGROUPS',
            link: '/definitions/glass-group',
            parentId: 10130,
          },
          {
            id: 10160,
            label: 'COLOR',
            link: '/definitions/color',
            parentId: 10130,
          },
          {
            id: 10170,
            label: 'COLORGROUPS',
            link: '/definitions/color-group',
            parentId: 10130,
          },
          {
            id: 180,
            label: 'TEMPLATECODES',
            link: '/definitions/template-codes',
            parentId: 10130,
          },
          {
            id: 10190,
            label: 'LENGTH',
            link: '/definitions/length',
            parentId: 10130,
          },
          {
            id: 10200,
            label: 'UNIT',
            link: '/definitions/unit',
            parentId: 10130,
          },
          {
            id: 10201,
            label: 'PRICELIST',
            link: '/definitions/price-list',
            parentId: 10130,
          },
        ],
      },
      {
        id: 10210,
        label: 'FIRMDEFINITIONSTITLE',
        parentId: 10020,
        subItems: [
          {
            id: 10220,
            label: 'FIRM',
            link: '/definitions/firm',
            parentId: 10210,
          },
          {
            id: 10230,
            label: 'FIRMTYPE',
            link: '/definitions/firm-type',
            parentId: 10210,
          },
          {
            id: 10235,
            label: 'DELIVERYADRESS',
            link: '/definitions/delivery-adress',
            parentId: 10210,
          },
        ],
      },

      {
        id: 10240,
        label: 'OTHERDEFINITIONSTITLE',
        parentId: 10020,
        subItems: [
          {
            id: 10250,
            label: 'CURRENCY',
            link: '/definitions/currency',
            parentId: 10240,
          },
          {
            id: 10260,
            label: 'EXCHANGERATE',
            link: '/definitions/exchange-rate',
            parentId: 10240,
          },
          {
            id: 10270,
            label: 'EXPENSE',
            link: '/definitions/expense',
            parentId: 10240,
          },
          {
            id: 10280,
            label: 'COUNTRY',
            link: '/definitions/country',
            parentId: 10240,
          },
          {
            id: 10281,
            label: 'REGION',
            link: '/definitions/region',
            parentId: 10240,
          },
          {
            id: 10282,
            label: 'CITY',
            link: '/definitions/city',
            parentId: 10240,
          },
          {
            id: 10283,
            label: 'COUNTY',
            link: '/definitions/county',
            parentId: 10240,
          },
          {
            id: 10284,
            label: 'DISTRICT',
            link: '/definitions/district',
            parentId: 10240,
          },
          {
            id: 10290,
            label: 'BANK',
            link: '/definitions/bank',
            parentId: 10240,
          },
        ],
      },
    ],
  },
  {
    id: 20000,
    label: 'PRODUCTION',
    isTitle: true,
  },
  {
    id: 20010,
    label: 'PRODUCTION',
    icon: 'ri-layout-grid-line',
    collapseid: 'sidebarProduction',
    subItems: [
      {
        id: 20020,
        label: 'FORMULATYPES',
        link: '/production/formula-type',
        parentId: 20010,
      },
      {
        id: 20030,
        label: 'FORMULA',
        link: '/production/formula',
        parentId: 20010,
      },
      {
        id: 20040,
        label: 'WORKORDER',
        link: '/production/work-order',
        parentId: 20010,
      },
    ],
  },
];

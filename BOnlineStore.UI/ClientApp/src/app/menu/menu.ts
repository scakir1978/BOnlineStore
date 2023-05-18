import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [
  {
    id: 10,
    label: 'DASHBOARD',
    icon: 'ri-dashboard-2-line',
  },
  {
    id: 20,
    label: 'DEFINITIONS',
    icon: 'ri-apps-2-line',
    collapseid: 'sidebarDefinitions',
    subItems: [
      {
        id: 30,
        label: 'MODELDEFINITIONSTITLE',
        isTitle: true,
        parentId: 20,
      },
      {
        id: 40,
        label: 'MODEL',
        link: '/definitions/model',
        parentId: 20,
      },
      {
        id: 50,
        label: 'PANEL',
        link: '/definitions/panel',
        parentId: 20,
      },
      {
        id: 60,
        label: 'MODELGROUPS',
        link: '/definitions/model-groups',
        parentId: 20,
        badge: {
          variant: 'bg-success',
          text: 'NEW',
        },
      },
      {
        id: 70,
        label: 'RAWMATERIALDEFINITIONSTITLE',
        isTitle: true,
        parentId: 20,
      },
      {
        id: 80,
        label: 'RAWMATERIAL',
        link: '/definitions/raw-material',
        parentId: 20,
      },
      {
        id: 90,
        label: 'RAWMATERIALGROUPS',
        link: '/definitions/raw-material-groups',
        parentId: 20,
      },
      {
        id: 100,
        label: 'ASSEMBLYDEFINITIONSTITLE',
        isTitle: true,
        parentId: 20,
      },
      {
        id: 110,
        label: 'MEASUREMENTASSEMBLYLIMITS',
        link: '/definitions/measurement-assembly-limits',
        parentId: 20,
      },
      {
        id: 115,
        label: 'ASSEMBLYPRICE',
        link: '/definitions/assembly-price',
        parentId: 20,
      },
      {
        id: 120,
        label: 'ASSEMBLER',
        link: '/definitions/assembler',
        parentId: 20,
      },
      {
        id: 130,
        label: 'PRODUCTIONDEFINITIONSTITLE',
        isTitle: true,
        parentId: 20,
      },
      {
        id: 135,
        label: 'RECIPETYPE',
        link: '/definitions/recipe-type',
        parentId: 20,
      },
      {
        id: 140,
        label: 'GLASS',
        link: '/definitions/glass',
        parentId: 20,
      },
      {
        id: 150,
        label: 'GLASSGROUPS',
        link: '/definitions/glass-group',
        parentId: 20,
      },
      {
        id: 160,
        label: 'COLOR',
        link: '/definitions/color',
        parentId: 20,
      },
      {
        id: 170,
        label: 'COLORGROUPS',
        link: '/definitions/color-group',
        parentId: 20,
      },
      {
        id: 180,
        label: 'TEMPLATECODES',
        link: '/definitions/template-codes',
        parentId: 20,
      },
      {
        id: 190,
        label: 'LENGTH',
        link: '/definitions/length',
        parentId: 20,
      },
      {
        id: 195,
        label: 'UNIT',
        link: '/definitions/unit',
        parentId: 20,
      },
      {
        id: 198,
        label: 'FIRMDEFINITIONSTITLE',
        isTitle: true,
        parentId: 20,
      },
      {
        id: 200,
        label: 'FIRM',
        link: '/definitions/firm',
        parentId: 20,
      },
      {
        id: 201,
        label: 'FIRMTYPE',
        link: '/definitions/firm-type',
        parentId: 20,
      },
      {
        id: 208,
        label: 'OTHERDEFINITIONSTITLE',
        isTitle: true,
        parentId: 20,
      },
      {
        id: 209,
        label: 'CURRENCY',
        link: '/definitions/currency',
        parentId: 20,
      },
      {
        id: 210,
        label: 'EXPENSE',
        link: '/definitions/expense',
        parentId: 20,
      },
      {
        id: 220,
        label: 'REGION',
        link: '/definitions/region',
        parentId: 20,
      },

      {
        id: 230,
        label: 'BANK',
        link: '/definitions/bank',
        parentId: 20,
      },
    ],
  },
  {
    id: 10000,
    label: 'PRODUCTION',
    isTitle: true,
  },
  {
    id: 10010,
    label: 'PRODUCTION',
    icon: 'ri-layout-grid-line',
    collapseid: 'sidebarProduction',
    subItems: [
      {
        id: 10020,
        label: 'FORMULATYPES',
        link: '/production/formula-type',
        parentId: 10010,
      },
    ],
  },
];

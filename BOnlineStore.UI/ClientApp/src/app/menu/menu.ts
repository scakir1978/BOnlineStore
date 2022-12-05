import { CoreMenu } from '@core/types';

export const menu: CoreMenu[] = [
  {
    id: 'home',
    title: 'Home',
    translate: 'MENU.HOME',
    type: 'item',
    icon: 'home',
    url: 'home',
  },
  {
    id: 'sample',
    title: 'Sample',
    translate: 'MENU.SAMPLE',
    type: 'item',
    icon: 'file',
    url: 'sample',
  },
  {
    id: 'definitions',
    title: 'Definitions',
    translate: 'KEYS.DEFINITIONS',
    type: 'collapsible',
    icon: 'book',
    children: [
      {
        id: 'modelgroups',
        title: 'Model Groups',
        translate: 'KEYS.MODELGROUPS',
        type: 'item',
        icon: 'file',
        url: 'definitions/model-groups',
      },
      {
        id: 'colorgroups',
        title: 'Color Groups',
        translate: 'KEYS.COLORGROUPS',
        type: 'item',
        icon: 'file',
        url: 'definitions/color-groups',
      },
      {
        id: 'color',
        title: 'Color',
        translate: 'KEYS.COLOR',
        type: 'item',
        icon: 'file',
        url: 'definitions/color',
      },
    ],
  },
];

import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Resumo',
    url: '/dashboard',
    icon: 'icon-grid',
  },
  {
    title: true,
    name: 'Principal',
  },
  {
    name: 'Empresas',
    url: '/companies',
    icon: 'icon-home',
  },
  {
    name: 'Processos',
    url: '/processes',
    icon: 'icon-folder',
  },
  {
    name: 'Notas Fiscais',
    url: '/invoices',
    icon: 'icon-docs',
  },
  {
    title: true,
    name: 'Ajuda',
  },
  {
    name: 'Sobre o Sistema',
    url: '/about',
    icon: 'icon-question',
  },
];

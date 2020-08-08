import { INavData } from '@coreui/angular';

export const navItems: INavData[] = buildNavItems();

function buildNavItems(): any {
  const user = JSON.parse(localStorage.getItem('user'));

  if(user && user.user.cargo.toUpperCase() === "ADMIN")
  {
    return [
      {
        name: 'Resumo',
        url: '/dashboard',
        icon: 'icon-home',
      },
      {
        title: true,
        name: 'Principal',
      },
      {
        name: 'Usuários',
        url: '/users',
        icon: 'icon-people',
      },
      {
        name: 'Empresas',
        url: '/companies',
        icon: 'icon-briefcase',
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
        name: 'Conversores',
        url: '/converters',
        icon: 'icon-home',
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
  }
  else {
    return [
      {
        name: 'Resumo',
        url: '/dashboard',
        icon: 'icon-home',
      },
      {
        title: true,
        name: 'Principal',
      },
      {
        name: 'Empresas',
        url: '/companies',
        icon: 'icon-briefcase',
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
        name: 'Conversores',
        url: '/converters',
        icon: 'icon-home',
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
  }
}
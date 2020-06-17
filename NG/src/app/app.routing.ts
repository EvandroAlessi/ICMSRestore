import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContainerComponent } from './pages/base/container/container.component';
import { UserGuard } from './guards/user.guard';
import { LoginComponent } from './pages/base/login/login.component';
import { NotFoundComponent } from './pages/base/404error/404.component';
import { ErrorComponent } from './pages/base/500error/500.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
    //canActivate: [UserGuard],
  },
  {
    path: '404',
    component: NotFoundComponent,
    data: {
      title: 'Erro 404',
    },
  },
  {
    path: '500',
    component: ErrorComponent,
    data: {
      title: 'Erro 500',
    },
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login',
    },
  },
  {
    path: '',
    component: ContainerComponent,
    canActivate: [UserGuard],
    data: {
      title: 'Resumo',
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./pages/dashboard/dashboard.module').then(
            (m) => m.DashboardModule
          ),
      },
      {
        path: 'companies',
        loadChildren: () =>
          import('./pages/companies/companies.module').then(
            (m) => m.CompaniesModule
          ),
      },
      {
        path: 'processes',
        loadChildren: () =>
          import('./pages/companies/companies.module').then(
            (m) => m.CompaniesModule
          ),
      },
      {
        path: 'invoices',
        loadChildren: () =>
          import('./pages/companies/companies.module').then(
            (m) => m.CompaniesModule
          ),
      },
      {
        path: 'about',
        loadChildren: () =>
          import('./pages/help/help.module').then((m) => m.HelpModule),
      },
    ],
  },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

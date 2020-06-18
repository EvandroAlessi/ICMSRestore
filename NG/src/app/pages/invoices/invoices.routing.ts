import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Notas Fiscais',
    },
    children: [
      {
        path: '',
        data: {
          title: '',
        },
        loadChildren: () =>
          import('./list/list.module').then((m) => m.ListModule),
      },
      {
        path: 'details',
        data: {
          title: '',
        },
        loadChildren: () =>
          import('./details/details.module').then((m) => m.DetailsModule),
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InvoicesRouting {}

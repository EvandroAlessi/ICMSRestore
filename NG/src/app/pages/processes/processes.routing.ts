import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Processos',
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
        path: 'edit',
        data: {
          title: '',
        },
        loadChildren: () =>
          import('./edit/edit.module').then((m) => m.EditModule),
      },
      {
        path: 'create',
        data: {
          title: '',
        },
        loadChildren: () =>
          import('./create/create.module').then((m) => m.CreateModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProcessesRouting {}

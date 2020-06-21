import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProcessTabsComponent } from './process-tabs.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: '',
    },
    children: [
      {
        path: ':id',
        component: ProcessTabsComponent,
        data: {
          title: 'Editar Processo',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProcessTabsRouting {}

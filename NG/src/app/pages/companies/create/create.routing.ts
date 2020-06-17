import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: '',
    },
    children: [
      {
        path: '',
        component: CreateComponent,
        data: {
          title: 'Incluir Departamento',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CreateRouting {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ItemsComponent } from './items.component';
import { ItemsRouting } from './items.routing';
import { CollapseModule } from 'ngx-bootstrap/collapse';

@NgModule({
  declarations: [ItemsComponent],
  imports: [
    CommonModule, 
    RouterModule, 
    ItemsRouting, 
    FormsModule,
    CollapseModule.forRoot(),
  ],
})
export class ItemsModule {}

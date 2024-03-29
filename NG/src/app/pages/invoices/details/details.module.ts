import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DetailsComponent } from './details.component';
import { DetailsRouting } from './details.routing';
import { ItemsModule } from './Items/items.module';
import { ItemsComponent } from './Items/items.component';

@NgModule({
  declarations: [DetailsComponent],
  imports: [
    CommonModule, 
    RouterModule, 
    DetailsRouting, 
    FormsModule,
    ItemsModule
  ],
  entryComponents: [ItemsComponent]
})
export class DetailsModule {}

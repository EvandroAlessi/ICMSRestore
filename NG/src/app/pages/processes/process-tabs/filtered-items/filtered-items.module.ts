import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FilteredItemsComponent } from './filtered-items.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [FilteredItemsComponent],
  imports: [
    CommonModule, 
    RouterModule, 
    FormsModule, 
    BsDropdownModule,
  ],
  exports: [FilteredItemsComponent]
})
export class FilteredItemsModule {}

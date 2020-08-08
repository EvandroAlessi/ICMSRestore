import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FilteredItemsComponent } from './filtered-items.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { DetailsComponent } from './details/details.component';
import { DetailsModule } from './details/details.module';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  declarations: [
    FilteredItemsComponent
  ],
  imports: [
    CommonModule, 
    RouterModule, 
    FormsModule, 
    BsDropdownModule,
    SharedModule,
    DetailsModule,
  ],
  exports: [
    FilteredItemsComponent
  ],
  entryComponents: [
    DetailsComponent, 
  ]
})
export class FilteredItemsModule {}

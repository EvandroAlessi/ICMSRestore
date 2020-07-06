import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProcessingComponent } from './processing.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  declarations: [ProcessingComponent],
  imports: [
    CommonModule, 
    RouterModule, 
    FormsModule, 
    BsDropdownModule,
    SharedModule,
  ],
  exports: [ProcessingComponent]
})
export class ProcessingModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UploadComponent } from './upload.component';
import { ProgressComponent } from '../../../shared/progress/progress.component';
import { DndDirective } from '../../../../dnd.directive';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [
    UploadComponent, 
    DndDirective, 
    ProgressComponent,
  ],
  imports: [
    CommonModule, 
    RouterModule, 
    FormsModule,
    BsDropdownModule,
  ],
  exports: [UploadComponent]
})
export class UploadModule {}

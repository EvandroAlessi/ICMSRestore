import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { EditComponent } from './edit.component';
import { EditRouting } from './edit.routing';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { DndDirective } from '../../../dnd.directive';
import { ProgressComponent } from '../../shared/progress/progress.component';

@NgModule({
  declarations: [EditComponent, DndDirective, ProgressComponent],
  imports: [
    CommonModule, 
    RouterModule,
    EditRouting, 
    FormsModule, 
    TabsModule,
    BsDropdownModule,

  ],
})
export class EditModule {}

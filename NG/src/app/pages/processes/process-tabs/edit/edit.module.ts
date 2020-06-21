import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { EditComponent } from './edit.component';

@NgModule({
  declarations: [EditComponent],
  imports: [
    CommonModule, 
    RouterModule,
    FormsModule, 
  ],
  exports: [EditComponent]
})
export class EditModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { EditComponent } from './edit.component';
import { EditRouting } from './edit.routing';

@NgModule({
  declarations: [EditComponent],
  imports: [CommonModule, RouterModule, EditRouting, FormsModule],
})
export class EditModule {}

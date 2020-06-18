import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DetailsComponent } from './details.component';
import { DetailsRouting } from './details.routing';

@NgModule({
  declarations: [DetailsComponent],
  imports: [CommonModule, RouterModule, DetailsRouting, FormsModule],
})
export class DetailsModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProcessesRouting } from './processes.routing';

@NgModule({
  imports: [CommonModule, FormsModule, RouterModule, ProcessesRouting],
})
export class ProcessesModule {}

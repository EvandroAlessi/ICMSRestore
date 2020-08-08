import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ConvertersRouting } from './converters.routing';

@NgModule({
  imports: [CommonModule, FormsModule, RouterModule, ConvertersRouting],
})
export class ConvertersModule {}

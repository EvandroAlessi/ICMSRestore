import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HelpRouting } from './help.routing';

@NgModule({
  imports: [CommonModule, HelpRouting, FormsModule],
})
export class HelpModule {}

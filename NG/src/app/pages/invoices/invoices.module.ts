import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoicesRouting } from './invoices.routing';

@NgModule({
  imports: [CommonModule, FormsModule, RouterModule, InvoicesRouting],
})
export class InvoicesModule {}

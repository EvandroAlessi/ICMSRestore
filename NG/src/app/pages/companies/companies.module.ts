import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CompaniesRouting } from './companies.routing';

@NgModule({
  imports: [CommonModule, FormsModule, RouterModule, CompaniesRouting],
})
export class CompaniesModule {}

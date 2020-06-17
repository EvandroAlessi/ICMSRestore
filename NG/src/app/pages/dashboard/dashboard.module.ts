import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DashboardRouting } from './dashboard.routing';

@NgModule({
  imports: [CommonModule, RouterModule, DashboardRouting, FormsModule],
})
export class DashboardModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MetricsComponent } from './metrics.component';
import { MetricsRouting } from './metrics.routing';

@NgModule({
  declarations: [MetricsComponent],
  imports: [CommonModule, RouterModule, MetricsRouting, FormsModule],
})
export class MetricsModule {}

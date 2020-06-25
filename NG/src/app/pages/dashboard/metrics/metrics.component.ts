import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MetricsService } from '../../../services/metrics.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './metrics.component.html',
  styles: [],
})
export class MetricsComponent implements OnInit {
  public route: string = '/metrics';
  public occurrences: any = [];
  public metrics: any = {};

  constructor(
    private metricsService: MetricsService
  ) {}

  ngOnInit() {
    this.metricsService.getCounts().subscribe((response) => {
        this.metrics = response;
    });
  }
}

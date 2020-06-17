import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './metrics.component.html',
  styles: [],
})
export class MetricsComponent implements OnInit {
  public route: string = '/occurrences';
  public occurrences: any = [];
  public metrics: any = {};

  constructor() {}

  ngOnInit() {
    // this.dashboardService.getAll([]).then((response) => {
    //   this.occurrences = response.occurrences;
    //   this.metrics = response.total;
    // });
  }
}

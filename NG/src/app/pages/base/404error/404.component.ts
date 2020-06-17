import { Component } from '@angular/core';

@Component({
  templateUrl: '404.component.html',
})
export class NotFoundComponent {
  constructor() {}

  back() {
    history.back();
  }
}

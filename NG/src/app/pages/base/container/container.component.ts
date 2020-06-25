import { Component, ElementRef, AfterViewInit } from '@angular/core';
import { navItems } from '../navigation';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './container.component.html',
})
export class ContainerComponent implements AfterViewInit {
  public sidebarMinimized = true;
  public navItems = navItems;

  constructor(
    private authenticationService: AuthenticationService,
    private elRef: ElementRef
  ) {}

  ngAfterViewInit() {
    this.elRef.nativeElement
      .querySelectorAll('.navbar-toggler')
      .forEach((element) => {
        element.addEventListener('click', () => {
          this.resizeEvent();
        });
      });
  }

  resizeEvent() {
    window.dispatchEvent(new Event('resize'));
    setTimeout(() => {
      this.resizeEvent();
    }, 300);
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
    this.resizeEvent();
  }

  logout() {
    this.authenticationService.logout();
  }
}

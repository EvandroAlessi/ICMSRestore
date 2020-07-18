import { Component, ElementRef, AfterViewInit, OnInit } from '@angular/core';
import { navItems } from '../navigation';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './container.component.html',
})
export class ContainerComponent implements OnInit, AfterViewInit {
  public sidebarMinimized = true;
  public navItems = navItems;
  public user: any = {};

  ngOnInit(){
      const user = JSON.parse(localStorage.getItem('user'));

      this.user = user.user;
  }

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

import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'body',
  template: '<router-outlet></router-outlet>'
})
export class AppComponent implements OnInit {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private toast: ToastrService
  ) {
    this.listenForLoginEvents();
  }

  ngOnInit() {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
    this.checkLoginStatus();
  }

  checkLoginStatus() {
    if (localStorage.getItem('user')) {
      this.updateLoggedInStatus(true);
    } else {
      this.updateLoggedInStatus(false);
    }
  }

  updateLoggedInStatus(loggedIn: boolean) {}

  listenForLoginEvents() {
    window.addEventListener('user:login', () => {
      this.updateLoggedInStatus(true);
    });
    window.addEventListener('user:logout', () => {
      this.updateLoggedInStatus(false);
    });
  }
}

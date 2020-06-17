import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html',
})
export class LoginComponent implements OnInit {
  public user = {
    username: '',
    password: '',
  };

  constructor(
    private router: Router,
    private authentication: AuthenticationService
  ) {}

  ngOnInit() {}

  login() {
    this.authentication.login(this.user).then((response) => {
      if (response.user) {
        this.router.navigate(['dashboard']);
      }
    });
  }
}

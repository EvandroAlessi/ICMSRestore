import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class UserGuard implements CanActivate {
  constructor(private authentication: AuthenticationService) {}

  canActivate() {
    if (localStorage.getItem('user')) {

      return true;
    }
    
    this.authentication.logout();

    return false;
  }
}

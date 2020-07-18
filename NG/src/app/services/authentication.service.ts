import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private api = environment.api.url + "/auth";

  constructor(private router: Router, private http: HttpClient) {}

  login(user): Promise<any> {
    const credentials = {
      Nome: user.username,
      Senha: user.password,
    };
    
    return this.http
      .post<any>(`${this.api}/login`, credentials, {})
      .pipe(
        map((response) => {
          if (response && response.user && response.user.token) {
            localStorage.setItem('user', JSON.stringify(response.user));

            window.dispatchEvent(new CustomEvent('user:login'));
          }

          return response;
        })
      )
      .toPromise();
  }

  logout() {
    this.http
      .post(`${this.api}/logout`, {})
      .toPromise()
      .then(() => {
        localStorage.removeItem('user');
        window.dispatchEvent(new CustomEvent('user:logout'));

        this.router.navigate(['/login']);
      })
      .catch(() => {
        localStorage.removeItem('user');
        window.dispatchEvent(new CustomEvent('user:logout'));

        this.router.navigate(['/login']);
      });
  }

  getUser() {
    let user = JSON.parse(localStorage.getItem('user'));

    if (!user) {
      user = {
        nome: 'Usuário',
      };
    }
    
    return user.user;
  }
}

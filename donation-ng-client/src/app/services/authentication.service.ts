import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { Usuario } from '../models/user';
import { UserAndToken } from '../models/user-and-token';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private api = environment.API.BASE_URL;

  constructor(private router: Router, private http: HttpClient) {}

  login(credentials: Usuario): Promise<UserAndToken> {
    return this.http
      .post<UserAndToken>(`${this.api}/login`, credentials, {})
      .pipe(
        map((userAndToken: UserAndToken) => {
          if (userAndToken && userAndToken.usuario && userAndToken.token) {
            localStorage.setItem('user', JSON.stringify(userAndToken));

            console.log('userAndToken', userAndToken);

            window.dispatchEvent(new CustomEvent('user:login'));
          }

          return userAndToken;
        })
      )
      .toPromise();
  }

  logout(): Promise<void> {

    return this.http
      .post(`${this.api}/logout`, {})
      .toPromise()
      .then(() => {
        window.dispatchEvent(new CustomEvent('user:logout'));
        localStorage.removeItem('user');
      })
      .catch(() => {
        window.dispatchEvent(new CustomEvent('user:logout'));
      });
  }

  getUser(): UserAndToken {
    let userAndToken: UserAndToken = new UserAndToken();
    const storedUser = localStorage.getItem('user') || null;

    if (storedUser) {
      userAndToken = JSON.parse(storedUser);
    }

    return userAndToken;
  }

  isAuthenticated(): boolean {
    const userAndToken = this.getUser();

    if (
      userAndToken.token == null ||
      userAndToken.token === '' ||
      userAndToken.token === undefined
    ) {
      return false;
    } else {
      return true;
    }
  }
}

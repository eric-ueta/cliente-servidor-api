import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Usuario } from '../models/user';

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {}

  getUsers(): Promise<Usuario[]> {
    let url = `${localStorage.getItem('server')}/api/users`;

    url = url.replace(/[?&]$/, '');

    return this.http.get<Usuario[]>(url, {}).toPromise();
  }

  update(user: Usuario): Promise<void> {
    const url = `${localStorage.getItem('server')}/api/users/${user.id}/update`;

    return this.http.put<void>(url, user, {}).toPromise();
  }

  create(user: Usuario): Promise<void> {
    const url = `${localStorage.getItem('server')}/usuarios`;

    return this.http.post<void>(url, user, {}).toPromise();
  }

  delete(userId: number): Promise<void> {
    const url = `${localStorage.getItem('server')}/api/users/${userId}/delete`;

    return this.http.delete<void>(url, {}).toPromise();
  }
}

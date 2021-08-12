import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable()
export class UserService {
  private baseUrl: string = environment.API.BASE_URL;

  constructor(private http: HttpClient) {}

  getUsers(): Promise<User[]> {
    let url = `${this.baseUrl}/api/users`;

    url = url.replace(/[?&]$/, '');

    return this.http.get<User[]>(url, {}).toPromise();
  }

  update(user: User): Promise<void> {
    const url = `${this.baseUrl}/api/users/${user.id}/update`;

    return this.http.put<void>(url, user, {}).toPromise();
  }

  create(user: User): Promise<void> {
    const url = `${this.baseUrl}/usuarios`;

    return this.http.post<void>(url, user, {}).toPromise();
  }

  delete(userId: number): Promise<void> {
    const url = `${this.baseUrl}/api/users/${userId}/delete`;

    return this.http.delete<void>(url, {}).toPromise();
  }
}

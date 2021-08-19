import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doacao } from '../models/donation';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class DonationService {
  constructor(
    private http: HttpClient,
    private authService: AuthenticationService
  ) {}

  getAll(): Promise<Doacao[]> {
    let url = `${localStorage.getItem('server')}/usuarios/${
      this.authService.getUser().usuario.id
    }/doacoes`;

    url = url.replace(/[?&]$/, '');

    return this.http.get<Doacao[]>(url, {}).toPromise();
  }

  get(id: any): Promise<Doacao> {
    let url = `${localStorage.getItem('server')}/doacoes/${id}`;

    url = url.replace(/[?&]$/, '');

    return this.http.get<Doacao>(url, {}).toPromise();
  }

  update(donation: Doacao): Promise<void> {
    const url = `${localStorage.getItem('server')}/doacoes`;

    return this.http.put<void>(url, donation, {}).toPromise();
  }

  create(donation: Doacao): Promise<void> {
    const url = `${localStorage.getItem('server')}/doacoes`;

    return this.http.post<void>(url, donation, {}).toPromise();
  }

  delete(id: number): Promise<void> {
    const url = `${localStorage.getItem('server')}/doacoes/${id}`;

    return this.http.delete<void>(url, {}).toPromise();
  }
}

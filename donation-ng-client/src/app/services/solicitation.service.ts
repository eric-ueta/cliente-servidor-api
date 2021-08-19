import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Solicitacao } from '../models/solicitation';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class SolicitationService {
  constructor(
    private http: HttpClient,
    private authService: AuthenticationService
  ) {}

  getAll(userId: any): Promise<Solicitacao[]> {
    let url = `${localStorage.getItem('server')}/usuarios/${
      this.authService.getUser().usuario.id
    }/solicitacoes`;

    url = url.replace(/[?&]$/, '');

    return this.http.get<Solicitacao[]>(url, {}).toPromise();
  }

  get(id: any): Promise<Solicitacao> {
    let url = `${localStorage.getItem('server')}/solicitacoes/${id}`;

    url = url.replace(/[?&]$/, '');

    return this.http.get<Solicitacao>(url, {}).toPromise();
  }

  update(solicitation: Solicitacao): Promise<void> {
    const url = `${localStorage.getItem('server')}/solicitacoes`;

    return this.http.put<void>(url, solicitation, {}).toPromise();
  }

  create(solicitation: Solicitacao): Promise<void> {
    const url = `${localStorage.getItem('server')}/solicitacoes`;

    return this.http.post<void>(url, solicitation, {}).toPromise();
  }

  delete(id: number): Promise<void> {
    const url = `${localStorage.getItem('server')}/solicitacoes/${id}`;

    return this.http.delete<void>(url, {}).toPromise();
  }
}

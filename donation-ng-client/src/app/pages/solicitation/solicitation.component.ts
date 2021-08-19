import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Solicitacao } from 'src/app/models/solicitation';
import { SolicitationService } from 'src/app/services/solicitation.service';
import { Form2Component } from './form/form.component';

@Component({
  selector: 'app-solicitation',
  templateUrl: './solicitation.component.html',
  styleUrls: ['./solicitation.component.scss'],
})
export class SolicitationComponent implements OnInit {
  solicitations = new Array<Solicitacao>();

  constructor(
    private solicitationService: SolicitationService,
    private toast: ToastrService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.solicitationService
      .getAll()
      .then((response) => {
        console.log(response);

        if (response) {
          this.solicitations = response;
        }
      })
      .catch((e) => {
        console.log(e);

        this.toast.error('Erro: ' + e.mensagem);
      });
  }

  create(): void {
    this.modalService?.show(Form2Component);
  }

  edit(solicitation: any): void {
    const aux = this.modalService?.show(Form2Component);

    if (aux.content) {
      aux.content.solicitation = solicitation;
    }
  }

  delete(id: any): any {
    this.solicitationService
      .delete(id)
      .then((response) => {
        console.log(response);

        window.location.reload();
      })
      .catch((e) => {
        console.log(e);

        this.toast.error('Erro: ' + e.mensagem);
      });
  }
}

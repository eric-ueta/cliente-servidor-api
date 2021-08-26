import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Doacao } from 'src/app/models/donation';
import { Usuario } from 'src/app/models/user';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DonationService } from 'src/app/services/donation.service';
import { UserService } from 'src/app/services/user.service';
import { Form3Component } from './form/form.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  users = new Array<Usuario>();

  constructor(
    private userService: UserService,
    private toast: ToastrService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.userService
      .getAll()
      .then((response) => {
        console.log(response);

        if (response) {
          this.users = response;
        }
      })
      .catch((e) => {
        console.log(e);

        this.toast.error('Erro: ' + e.mensagem);
      });
  }

  create(): void {
    this.modalService?.show(Form3Component);
  }

  edit(donation: any): void {
    const aux = this.modalService?.show(Form3Component);

    if (aux.content) {
      aux.content.user = donation;
    }
  }

  delete(id: any): any {
    this.userService
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

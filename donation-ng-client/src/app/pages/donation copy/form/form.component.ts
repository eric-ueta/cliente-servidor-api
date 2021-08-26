import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Doacao } from 'src/app/models/donation';
import { Usuario } from 'src/app/models/user';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DonationService } from 'src/app/services/donation.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class Form3Component implements OnInit {
  user = new Usuario();
  selectedDonations: any;

  constructor(
    private userService: UserService,
    private authService: AuthenticationService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {
    if (this.user.tipo_doacao) {
      this.selectedDonations = this.user.tipo_doacao;
    }
  }

  save(): void {
    // this.donation.tipo_doacao = this.selectedDonations; //?.join(',') ?? '';
    console.log(this.user);
    if (this.user.id != 0) {
      this.userService
        .update(this.user)
        .then((response) => {
          console.log(response);

          window.location.reload();
        })
        .catch((e) => {
          console.log(e);

          this.toast.error('Erro: ' + e.mensagem);
        });
    } else {
      this.userService
        .create(this.user)
        .then((response) => {
          console.log(response);
          this.toast.success('Usuário criado com Sucesso');
        })
        .catch((error) => {
          console.log(error);
          this.toast.error('Error ao criar usuário');
        });
    }
  }
}

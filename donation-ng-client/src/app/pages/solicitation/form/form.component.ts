import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Doacao } from 'src/app/models/donation';
import { Solicitacao } from 'src/app/models/solicitation';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DonationService } from 'src/app/services/donation.service';
import { SolicitationService } from 'src/app/services/solicitation.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class Form2Component implements OnInit {
  solicitation = new Solicitacao();

  constructor(
    private solicitationService: SolicitationService,
    private authService: AuthenticationService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {}

  save(): void {
    if (this.solicitation.id) {
      this.solicitation.receptorId = this.authService.getUser().usuario.id;

      this.solicitationService
        .update(this.solicitation)
        .then((response) => {
          console.log(response);

          window.location.reload();
        })
        .catch((e) => {
          console.log(e);

          this.toast.error('Erro: ' + e.mensagem);
        });
    } else {
      this.solicitation.receptorId = this.authService.getUser().usuario.id;

      this.solicitationService
        .create(this.solicitation)
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
}

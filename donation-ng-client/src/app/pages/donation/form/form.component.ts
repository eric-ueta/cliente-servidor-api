import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Doacao } from 'src/app/models/donation';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DonationService } from 'src/app/services/donation.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  donation = new Doacao();
  selectedDonations: any;

  constructor(
    private donationService: DonationService,
    private authService: AuthenticationService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {
    if (this.donation.tipo_doacao) {
      this.selectedDonations = this.donation.tipo_doacao;
    }
  }

  save(): void {
    // this.donation.tipo_doacao = this.selectedDonations; //?.join(',') ?? '';

    if (this.donation.id) {
      this.donation.doadorId = this.authService.getUser().usuario.id;

      this.donationService
        .update(this.donation)
        .then((response) => {
          console.log(response);

          window.location.reload();
        })
        .catch((e) => {
          console.log(e);

          this.toast.error('Erro: ' + e.mensagem);
        });
    } else {
      this.donation.doadorId = this.authService.getUser().usuario.id;

      this.donationService
        .create(this.donation)
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

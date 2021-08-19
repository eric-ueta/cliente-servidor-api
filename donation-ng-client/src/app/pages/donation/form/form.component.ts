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

  constructor(
    private donationService: DonationService,
    private authService: AuthenticationService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {}

  save(): void {
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

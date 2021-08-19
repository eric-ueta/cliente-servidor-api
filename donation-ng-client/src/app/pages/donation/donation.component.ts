import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Doacao } from 'src/app/models/donation';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { DonationService } from 'src/app/services/donation.service';
import { FormComponent } from './form/form.component';

@Component({
  selector: 'app-donation',
  templateUrl: './donation.component.html',
  styleUrls: ['./donation.component.scss'],
})
export class DonationComponent implements OnInit {
  donations = new Array<Doacao>();

  constructor(
    private donationService: DonationService,
    private toast: ToastrService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.donationService
      .getAll()
      .then((response) => {
        console.log(response);

        if (response) {
          this.donations = response;
        }
      })
      .catch((e) => {
        console.log(e);

        this.toast.error('Erro: ' + e.mensagem);
      });
  }

  create(): void {
    this.modalService?.show(FormComponent);
  }

  edit(donation: any): void {
    const aux = this.modalService?.show(FormComponent);

    if (aux.content) {
      aux.content.donation = donation;
    }
  }

  delete(id: any): any {
    this.donationService
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

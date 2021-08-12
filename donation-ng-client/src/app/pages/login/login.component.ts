import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { Usuario } from 'src/app/models/user';
import { ToastrService } from 'ngx-toastr';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'app-login',
  styleUrls: ['login.component.scss'],
  templateUrl: 'login.component.html',
})
export class LoginComponent implements OnInit {
  public user: Usuario;
  invalidCredentials: boolean;
  server = localStorage.getItem('server') || '';

  constructor(
    private router: Router,
    private authentication: AuthenticationService,
    private toast: ToastrService,
    private modalService: BsModalService
  ) {
    this.invalidCredentials = false;
    this.user = new Usuario();
  }

  ngOnInit(): void {
    // if (this.authentication.isAuthenticated()) {
    //   this.authentication.logout();
    // }
  }

  addNewUser(): void {
    console.log('here');
    this.modalService?.show(UserComponent);
  }

  setServer(e: any): void {
    localStorage.setItem('server', this.server);
  }

  login(): void {
    this.authentication
      .login(this.user)
      .then((response) => {
        console.log(response);

        this.router.navigate(['/']);
      })
      .catch((e) => {
        console.log(e);

        this.invalidCredentials = true;
        this.toast.error(
          'Verifique seu usuário e senha antes de tentar novamente.'
        );
      });
  }
}

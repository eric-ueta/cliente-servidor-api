import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { User } from 'src/app/models/user';
import { ToastrService } from 'ngx-toastr';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UserComponent } from '../user/user.component';


@Component({
  selector: 'app-login',
  styleUrls: ['login.component.scss'],
  templateUrl: 'login.component.html',
})
export class LoginComponent implements OnInit {
  public user: User;
  invalidCredentials: boolean;

  constructor(
    private router: Router,
    private authentication: AuthenticationService,
    private toast: ToastrService,
    private modalService: BsModalService,
  ) {
    this.invalidCredentials = false;
    this.user = new User();
  }

  ngOnInit(): void {
    if (this.authentication.isAuthenticated()) {
      this.authentication.logout();
    }
  }

  addNewUser():void{
    console.log("here")
    this.modalService
    ?.show(UserComponent);
  }

  login(): void {
    this.authentication
      .login(this.user)
      .then((response) => {
        if (this.authentication.isAuthenticated()) {
          this.router.navigate(['']);
        }
      })
      .catch((e) => {
        this.invalidCredentials = true;
        this.toast.error(
          'Verifique seu usuário e senha antes de tentar novamente.'
        );
      });
  }
}

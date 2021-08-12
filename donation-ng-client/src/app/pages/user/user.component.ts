import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  user: User;

  constructor(private userService: UserService,
    private toast: ToastrService) { 
    this.user = new User();
  }

  ngOnInit(): void {
  }

  saveUser():void {
    this.userService.create(this.user)
      .then(()=> this.toast.success("Usuário criado com Sucesso"))
      .catch((error)=> this.toast.error("Error ao criar usuário"));
  }
}

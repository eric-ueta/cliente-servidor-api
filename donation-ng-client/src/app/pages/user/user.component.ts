// import { Component, OnInit } from '@angular/core';
// import { ToastrService } from 'ngx-toastr';
// import { Usuario } from 'src/app/models/user';
// import { UserService } from 'src/app/services/user.service';

// @Component({
//   selector: 'app-user',
//   templateUrl: './user.component.html',
//   styleUrls: ['./user.component.scss'],
// })
// export class UserComponent implements OnInit {
//   user: Usuario;
//   selectedDonations: any;

//   constructor(private userService: UserService, private toast: ToastrService) {
//     this.user = new Usuario();
//   }

//   ngOnInit(): void {}

//   saveUser(): void {
//     this.user.tipo_doacao = this.selectedDonations?.join(',') ?? '';

//     this.userService
//       .create(this.user)
//       .then((response) => {
//         console.log(response);
//         this.toast.success('Usuário criado com Sucesso');
//       })
//       .catch((error) => {
//         console.log(error);
//         this.toast.error('Error ao criar usuário');
//       });
//   }
// }

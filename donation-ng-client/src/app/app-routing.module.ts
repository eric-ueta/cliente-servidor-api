import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { UserGuard } from './guards/user.guard';
import { UserComponent } from './pages/donation copy/user.component';
import { DonationComponent } from './pages/donation/donation.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { SolicitationComponent } from './pages/solicitation/solicitation.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [UserGuard],
  },
  {
    path: 'doacoes',
    component: DonationComponent,
    canActivate: [UserGuard],
  },
  {
    path: 'solicitacoes',
    component: SolicitationComponent,
    canActivate: [UserGuard],
  },
  {
    path: 'usuarios',
    component: UserComponent,
    canActivate: [UserGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule],
  declarations: [],
})
export class AppRoutingModule {}

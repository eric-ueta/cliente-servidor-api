import {
  Component,
  OnInit,
  ElementRef,
  EventEmitter,
  Output,
} from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./sass/navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  @Output() changeNavView = new EventEmitter<boolean>();
  vertical = true;
  selected = 0;
  userType = 0;

  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    if (this.router.url.includes('doacoes')) {
      this.selected = 1;
    } else if (this.router.url.includes('solicitacoes')) {
      this.selected = 2;
    } else if (this.router.url.includes('usuarios')) {
      this.selected = 3;
    } else {
      this.selected = 0;
    }

    this.userType = this.authService.getUser().usuario.tipo;
  }

  logout(): void {
    this.authService.logout().finally(() => {
      this.router.navigate(['/login']);
    });
  }

  changeNav(): void {
    this.vertical = !this.vertical;
    this.changeNavView.emit(this.vertical);
  }
}

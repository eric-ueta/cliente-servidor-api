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

  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  ngOnInit() {
    if (this.router.url.includes('doacoes')) {
      this.selected = 1;
    } else if (this.router.url.includes('solicitacoes')) {
      this.selected = 2;
    } else {
      this.selected = 0;
    }
  }

  logout() {
    this.authService.logout().finally(() => {
      this.router.navigate(['/login']);
    });
  }

  changeNav() {
    this.vertical = !this.vertical;
    this.changeNavView.emit(this.vertical);
  }
}

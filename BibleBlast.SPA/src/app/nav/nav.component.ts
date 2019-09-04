import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router, NavigationStart } from '@angular/router';
import { filter } from 'rxjs/internal/operators/filter';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  isCollapsed = true;
  // todo
  loginModel: any = {};

  constructor(public authService: AuthService, private router: Router) {
    this.router.events.pipe(filter(e => e instanceof NavigationStart))
      .subscribe(() => this.isCollapsed = true);
  }

  login() {
    this.authService.login(this.loginModel).subscribe((next) => {
      console.log('Logged in successfully');
    }, error => {
      console.error(error);
    }, () => {
      this.router.navigate(['/dashboard']);
    });
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');

    this.authService.decodedToken = null;
    this.authService.currentUser = null;

    this.router.navigate(['/home']);
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}

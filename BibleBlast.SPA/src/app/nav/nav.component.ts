import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  loginModel: any = {};

  constructor(public authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.loginModel).subscribe((next) => {
      console.log('Logged in successfully');
    }, error => {
      console.error(error);
    }, () => {
      this.router.navigate(['/kids']);
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

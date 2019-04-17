import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = `${environment.apiUrl}/auth`;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;

  constructor(private http: HttpClient) { }

  login(user: User) {
    return this.http.post(`${this.baseUrl}/login`, user)
      .pipe(map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.token);
          localStorage.setItem('user', JSON.stringify(response.user));
          this.decodedToken = this.jwtHelper.decodeToken(response.token);
          this.currentUser = response.user;
        }
      }));
  }

  register(user: User) {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  userHasRole(roles: string[]): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    roles.forEach((role: string) => {
      if (userRoles.includes(role)) {
        isMatch = true;
        return;
      }
    });

    return isMatch;
  }

  resetPassword(userId: number, password: string) {
    return this.http.post(`${this.baseUrl}/reset-password`, { userId, password });
  }
}

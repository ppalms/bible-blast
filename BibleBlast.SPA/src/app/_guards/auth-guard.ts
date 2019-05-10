import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router, CanActivate, ActivatedRouteSnapshot, UrlTree } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private auth: AuthService, private router: Router, private alertify: AlertifyService) { }

    canActivate(next: ActivatedRouteSnapshot): boolean | UrlTree {
        const roles = next.firstChild.data.roles as Array<string>;
        if (roles) {
            const hasRole = this.auth.userHasRole(roles);
            if (hasRole) {
                return true;
            } else {
                this.router.navigate(['/home']);
                console.error('You are not authorized to access this area');
            }
        }

        if (this.auth.isLoggedIn()) {
            return true;
        }

        this.alertify.error('Please log in');
        return this.router.parseUrl('home');
    }
}

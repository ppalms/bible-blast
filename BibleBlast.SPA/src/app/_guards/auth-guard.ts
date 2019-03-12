import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private auth: AuthService, private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot): boolean {
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

        console.error('Please log in');
        this.router.navigate(['/home']);
        return false;
    }
}

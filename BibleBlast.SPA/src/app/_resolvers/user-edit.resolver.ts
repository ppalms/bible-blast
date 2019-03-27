import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { catchError } from 'rxjs/operators';

@Injectable()
export class UserEditResolver implements Resolve<User> {
    constructor(private userService: UserService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<User> {
        return this.userService.getUser(route.params.id).pipe(
            catchError(() => {
                console.error('Problem retrieving data');
                this.router.navigate(['/users']);
                return of(null);
            })
        );
    }
}

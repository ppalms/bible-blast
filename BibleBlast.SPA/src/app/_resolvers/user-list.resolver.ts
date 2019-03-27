import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';

@Injectable()
export class UserListResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private userService: UserService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.getUsers(this.pageNumber, this.pageSize).pipe(
            catchError(() => {
                console.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}

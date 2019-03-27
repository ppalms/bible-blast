import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Kid } from '../_models/kid';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { KidService } from '../_services/kid.service';

@Injectable()
export class KidListResolver implements Resolve<Kid[]> {
    pageNumber = 1;
    pageSize = 6;

    constructor(private kidService: KidService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Kid[]> {
        return this.kidService.getKids(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                console.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}

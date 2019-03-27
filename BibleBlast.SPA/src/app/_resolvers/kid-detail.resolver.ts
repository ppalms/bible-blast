import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { KidService } from '../_services/kid.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Kid } from '../_models/kid';

@Injectable()
export class KidDetailResolver implements Resolve<Kid> {
    constructor(private kidService: KidService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Kid> {
        return this.kidService.getKid(route.params.id).pipe(
            catchError(error => {
                console.error('Problem retrieving data');
                this.router.navigate(['/kids']);
                return of(null);
            })
        );
    }
}

import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AwardCategory } from '../_models/award';
import { AwardService } from '../_services/award.service';

@Injectable()
export class AwardListResolver implements Resolve<AwardCategory[]> {
    constructor(private awardService: AwardService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<AwardCategory[]> {
        return this.awardService.getAwards().pipe(
            catchError(() => {
                console.error('Problem retrieving data');
                this.router.navigate(['/awards']);
                return of(null);
            })
        );
    }
}

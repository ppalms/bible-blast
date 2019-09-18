import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Award } from '../_models/award';
import { AwardService } from '../_services/award.service';

@Injectable()
export class AwardEditResolver implements Resolve<Award> {
    constructor(private awardService: AwardService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Award> {
        return this.awardService.getAward(route.params.id).pipe(
            catchError(() => {
                console.error('Problem retrieving data');
                this.router.navigate(['/awards']);
                return of(null);
            })
        );
    }
}

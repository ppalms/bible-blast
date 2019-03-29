import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { OrganizationService } from '../_services/organization.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Organization } from '../_models/organization';

@Injectable()
export class OrganizationListResolver implements Resolve<Organization[]> {
    constructor(private organizationService: OrganizationService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Organization[]> {
        return this.organizationService.getOrganizations().pipe(
            catchError(error => {
                console.error('Problem retrieving data');
                this.router.navigate(['/users']);
                return of(null);
            })
        );
    }
}

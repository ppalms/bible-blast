import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MemoryCategory } from '../_models/memory';
import { MemoryService } from '../_services/memory.service';

@Injectable()
export class MemoryCategoryResolver implements Resolve<MemoryCategory[]> {
    constructor(private memoryService: MemoryService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<MemoryCategory[]> {
        return this.memoryService.getMemoriesByCategory().pipe(
            catchError(error => {
                console.error('Problem retrieving data');
                this.router.navigate(['/kids']);
                return of(null);
            })
        );
    }
}

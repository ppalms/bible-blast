import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpParams, HttpClient } from '@angular/common/http';
import { Kid } from '../_models/kid';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class KidService {
  constructor(private http: HttpClient) { }

  getKids(page?, itemsPerPage?): Observable<PaginatedResult<Kid[]>> {
    const paginatedResult: PaginatedResult<Kid[]> = new PaginatedResult<Kid[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Kid[]>(`${environment.apiUrl}/kids`, { observe: 'response', params })
      .pipe(map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      }));
  }
}

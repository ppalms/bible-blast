import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { Kid } from '../_models/kid';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class KidService {
  constructor(private http: HttpClient) { }

  getKid(id: number): any {
    return this.http.get<Kid>(`${environment.apiUrl}/kids/${id}`);
  }

  getKids(page?: number, itemsPerPage?: number, kidParams?: any): Observable<PaginatedResult<Kid[]>> {
    const paginatedResult: PaginatedResult<Kid[]> = new PaginatedResult<Kid[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (kidParams && kidParams.name) {
      params = params.append('kidName', kidParams.name);
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

  insertKid(kid: Kid) {
    return this.http.post(`${environment.apiUrl}/kids`, kid);
  }

  updateKid(kid: Kid) {
    return this.http.patch(`${environment.apiUrl}/kids/${kid.id}`, kid);
  }

  deleteKid(id: number) {
    return this.http.delete(`${environment.apiUrl}/kids/${id}`);
  }

  upsertKidMemories(id: number, kidMemories: any[]) {
    return this.http.post(`${environment.apiUrl}/kids/${id}/memories`, kidMemories);
  }

  deleteKidMemories(id: number, kidMemories: any[]) {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      body: kidMemories,
    };

    return this.http.delete(`${environment.apiUrl}/kids/${id}/memories`, options);
  }
}

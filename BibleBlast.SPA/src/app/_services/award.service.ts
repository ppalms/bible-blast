import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Award, AwardCategory, AwardEarned } from '../_models/award';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AwardService {
  constructor(private http: HttpClient) { }

  getAwards() {
    return this.http.get<AwardCategory[]>(`${environment.apiUrl}/awards`);
  }

  getAward(id: number): Observable<Award> {
    return this.http.get<Award>(`${environment.apiUrl}/awards/${id}`);
  }

  getAwardsEarned(categoryId: number): Observable<AwardEarned[]> {
    let params = new HttpParams();
    params = params.append('categoryId', categoryId.toString());

    return this.http.get<AwardEarned[]>(`${environment.apiUrl}/awards/earned`, { params });
  }
}

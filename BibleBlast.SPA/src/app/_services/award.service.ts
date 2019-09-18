import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Award, AwardCategory } from '../_models/award';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AwardService {
  constructor(private http: HttpClient) { }

  getAwards() {
    return this.http.get<AwardCategory[]>(`${environment.apiUrl}/awards`);
  }

  getAward(id: number): any {
    return this.http.get<Award>(`${environment.apiUrl}/awards/${id}`);
  }
}

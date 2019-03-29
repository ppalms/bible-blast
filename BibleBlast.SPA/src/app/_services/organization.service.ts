import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Organization } from '../_models/organization';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {
  constructor(private http: HttpClient) { }

  getOrganizations(): Observable<Organization[]> {
    return this.http.get<Organization[]>(`${environment.apiUrl}/organizations`);
  }
}

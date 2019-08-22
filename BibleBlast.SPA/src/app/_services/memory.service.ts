import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { MemoryCategory } from '../_models/memory';

@Injectable({
  providedIn: 'root'
})
export class MemoryService {
  constructor(private http: HttpClient) { }

  getMemoriesByCategory(): Observable<MemoryCategory[]> {
    return this.http.get<MemoryCategory[]>(`${environment.apiUrl}/memories/byCategory`);
  }
}

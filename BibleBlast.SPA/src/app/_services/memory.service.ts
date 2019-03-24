import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CompletedMemory } from '../_models/kid';
import { Observable } from 'rxjs';
import { KidMemoryCategory } from '../_models/memory';

@Injectable({
  providedIn: 'root'
})
export class MemoryService {
  constructor(private http: HttpClient) { }

  getMemoriesByCategory(): Observable<KidMemoryCategory[]> {
    return this.http.get<KidMemoryCategory[]>(`${environment.apiUrl}/memories/byCategory`);
  }
}

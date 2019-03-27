import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  getUsers(pageNumber: number, pageSize: number): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}/users`);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
  }

  updateUser(user: User) {
    const dto = {
      firstName: user.firstName,
      lastName: user.lastName,
      organizationId: user.organization.id
    };

    return this.http.patch(`${environment.apiUrl}/users/${user.id}`, dto);
  }
}

import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environment';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  login(user: User) {
    let params = new HttpParams().set('User', JSON.stringify(user));
    return this.http.post(this.baseUrl + 'user/login', user);
  }
}

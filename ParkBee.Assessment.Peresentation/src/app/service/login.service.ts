import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}

  loginController: string = 'http://localhost:49835/token';
  login(username: string, password: string): Observable<object> {
    return this.http.post(this.loginController, {
      username: username,
      password: password,
    });
  }
}

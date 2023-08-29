import { Inject, Injectable } from '@angular/core';
import { Register } from '../interfaces/register';
import { HttpClient } from '@angular/common/http';
import { EMPTY, catchError } from 'rxjs';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  accessToken: string = 'access-token';

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router
  ) { }

  login(username: string, password: string) {

    this.http.post<any>(this.baseUrl + 'user/login', { username, password }).
      pipe(
        catchError(err => {
          console.log(err);
          return EMPTY;
        })
      )
      .subscribe(res => {
        this.setSession(res.token);
        this.router.navigate(['']);
      });

  }



  register(data: Register) {
    this.http.post<any>(this.baseUrl + 'user/register', data).
      pipe(
        catchError(err => {
          console.log(err);
          return EMPTY;
        })
      )
      .subscribe(res => {
        console.log(res);
      });

  }

  logout() {
    localStorage.removeItem(this.accessToken);
  }

  isLoggedIn() {
    return !!localStorage.getItem(this.accessToken);

  }

  checklogin() {
    if (!!localStorage.getItem(this.accessToken)) {
      return true;

    } else {
      return false;
    };
  }

  setSession(token: string) {
    localStorage.setItem(this.accessToken, token);
  }

  getAuthorizationToken(): string {
    return localStorage.getItem(this.accessToken) || '';
  }

  checkisadmin() {
    console.log(localStorage.getItem(this.accessToken) ?? '');
  }

  checkisuser() {

  }

  checkisshop() {

  }


  hasPermission(permissions: string[]): boolean {
    if (!this.isLoggedIn()) {
      return false;
    }

    if (permissions.length === 0) {
      return true;
    }

    let token = jwt_decode<any>(localStorage.getItem(this.accessToken) ?? '');

    if (!token) {
      return false;
    }

    let hasPermission = true;

    for (let index = 0; index < permissions.length; index++) {
      const permission = permissions[index];

      console.log();
      if (!token[permission]) {
        hasPermission = false;
        break;
      }
    }

    return hasPermission;
  }
}





import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private Auth: AuthService) {

  }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    switch (route.routeConfig?.path) {
      case 'login': return !this.Auth.checklogin();
      case 'register': return !this.Auth.checklogin();
      default: this.router.navigate(['login']); return true;
    }

  }

}
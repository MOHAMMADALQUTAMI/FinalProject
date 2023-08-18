import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {

  }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const hasPermissions = route.data['checkstatus']; // Get resolved data

    console.log(route.routeConfig?.path); // Add this line

    switch (route.routeConfig?.path) {
      case 'basket': return true;
      default: this.router.navigate(['login']); return false;
    }
  }

}

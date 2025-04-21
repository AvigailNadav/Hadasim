import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { AuthService } from '../auth/auth-service/auth.service';
import { UserRole } from '../models/user-role.enum';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route:ActivatedRouteSnapshot): boolean {
    const role=route.data['role'] as UserRole;
    const current = this.authService.getRole();

    if (role !== current) {
        this.router.navigate(['/unauthorized'])
      return false;
    }
    return true;
  }
}

import { Component, OnInit } from '@angular/core';
import { UserRole } from '../../models/user-role.enum';
import { AuthService } from '../../auth/auth-service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
  userRole:UserRole|null=null;

  constructor(private authService:AuthService,private router:Router){}
  ngOnInit(): void {
    const role = this.authService.getRole();
    this.userRole=role?role as UserRole:null;
  }
  logout():void{
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UserRole } from '../../models/user-role.enum';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl='https://localhost:7011/api/Supplier';

  constructor(private http:HttpClient,private router:Router) { }

  login(credentials:{phoneNumber:string,password:string}){
    return this.http.post<any>(`${this.baseUrl}/login`,credentials);
  }
  register(data:any){
    return this.http.post<any>(`${this.baseUrl}/registerSupplier`,data);
  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.router.navigate(['/login']);
  }
  saveToken(token:string,role:string){
    localStorage.setItem('token',token);
    localStorage.setItem('role',role);
  }
  getToken(): string | null {
    return localStorage.getItem('token');
  }
  getRole():UserRole|null{
    const role= localStorage.getItem('role');
    if (Object.values(UserRole).includes(role as UserRole)){
      return role as UserRole;
    }
    return null
  }
  
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getSupplierIdFromToken(): number | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return Number(payload.nameid); 
    } catch (err) {
      console.error('שגיאה בפענוח JWT:', err);
      return null;
    }
  }

}

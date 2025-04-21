import { Component, OnInit } from '@angular/core';
import { Supplier } from '../../models/Supplier.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SupplierService } from '../../services/supplier-service/supplier.service';
import { AuthService } from '../../auth/auth-service/auth.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-supplier-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './supplier-dashboard.component.html',
  styleUrl: './supplier-dashboard.component.css'
})
export class SupplierDashboardComponent implements OnInit{
  supplier:Supplier|null=null;

  constructor(
    private router:Router,
    private supplierService: SupplierService,
    private authService:AuthService
  ){}

  ngOnInit(): void {
    const token=this.authService.getToken();
    if(!token){
      console.error('No token found');
      this.router.navigate(['/login']);
      return;
    }
    try{
      const decodedToken=jwtDecode<any>(token);
      const supplierId=decodedToken.supplierId;
      if(!supplierId){
        console.error('No supplierId found in token');
        this.router.navigate(['/login']);
        return;
      }
      this.supplierService.getSupplierById(supplierId).subscribe({
        next:(data)=>{
          this.supplier=data;
          localStorage.setItem('supplier',JSON.stringify(data));
        },
        error:(err)=>{
          console.error('Failed to fetch supplier',err)
        }
      });
    }catch(error){
      console.error('Error decoding token ',error);
      this.router.navigate(['/login']);
    }
  }
    navigateToAddProduct(): void {
      this.router.navigate(['/supplier/add-product']);
    }
    navigateToMyProducts(): void {
      this.router.navigate(['/supplier/my-products']);
    }
}

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { SupplierService } from '../../services/supplier-service/supplier.service';

@Component({
  selector: 'app-suppliers',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './suppliers.component.html',
  styleUrls:['./suppliers.component.css']
})
export class SuppliersComponent implements OnInit{
  suppliers:any[]=[];

  constructor(private supplierService:SupplierService){}

  ngOnInit(): void {
    this.supplierService.getAllSuppliers().subscribe(data=>{
      this.suppliers=data;
    });
  }

}

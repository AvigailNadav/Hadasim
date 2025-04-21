import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/Product.model';
import { ProductService } from '../../services/product-service/product.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit{
  products:Product[]=[];
  constructor(private productService:ProductService){}

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(data=>{
      this.products=data;
    });
  }

}

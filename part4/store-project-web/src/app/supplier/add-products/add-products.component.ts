import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/Product.model';
import { ProductService } from '../../services/product-service/product.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-add-products',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './add-products.component.html',
  styleUrls: ['./add-products.component.css']
})
export class AddProductsComponent implements OnInit{
  newProduct:Product={};

  constructor(
    private productService:ProductService,
  ){}
  ngOnInit(): void {}
  
  addProduct(): void {
    if (
      !this.newProduct.Name || this.newProduct.Name.trim().length === 0 ||
      this.newProduct.Price == null ||
      this.newProduct.MinPurchase == null
    ) {
      console.error('יש לוודא שכל השדות מלאים');
      return;
    }
    this.newProduct.Price = Number(this.newProduct.Price);
    this.newProduct.MinPurchase = Number(this.newProduct.MinPurchase);
    
    console.log('שולח מוצר:', this.newProduct);

    this.productService.addProduct(this.newProduct).subscribe(
      (response) => {
        console.log('המוצר נוסף בהצלחה', response);
        this.newProduct = {};
      },
      (error) => {
        console.error('שגיאה בהוספת המוצר', error);
      }
    );
  }

}

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OrderDetails } from '../../models/OrderDetails.model';
import { SupplierService } from '../../services/supplier-service/supplier.service';
import { ProductsComponent } from '../products/products.component';
import { GeneralService } from '../../services/general-service/general.service';
import { Order } from '../../models/Order.model';
import { SupplierProductService } from '../../services/supplier-product/supplier-product.service';

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})
export class NewOrderComponent implements OnInit {
  suppliers: any[] = [];
  selectedSupplierId: number | null = null;
  products: any[] = [];
  selectedProductId: number | null = null;
  quantity: number = 1;
  orderDetails: OrderDetails[] = [];

  constructor(
    private supplierService: SupplierService,
    private supplierProductService: SupplierProductService,
    private generalService: GeneralService<Order>
  ) { }

  ngOnInit(): void {
    this.loadSuppliers();
  }
  loadSuppliers(): void {
    this.supplierService.getAllSuppliers().subscribe((res) => {
      this.suppliers = res;
    });
  }
  onSupplierChange(): void {
    this.orderDetails = [];
    this.selectedProductId = null;
    if (this.selectedSupplierId) {
      this.supplierProductService.getProductsBySupplierWithDetails(this.selectedSupplierId)
        .subscribe((res) => {
          this.products = res;
        });
    } else {
      this.products = [];
    }
  }
  addToOrder(): void {
    const product = this.products.find(p => p.Id === this.selectedProductId);
    if (!product || this.quantity <= 0) {
      alert('יש לבחור מוצר וכמות חוקית');
      return;
    }
    this.orderDetails.push({
      productId:product.Id,
      quantity:this.quantity,
      price:product.price,
      totalPrice:product.price*this.quantity
    });
    this.selectedProductId=null;
    this.quantity=1;
  }
  submitOrder():void{
    if(!this.selectedSupplierId||this.orderDetails.length===0){
      alert('יש לבחור ספק ולהוסיף לפחות מוצר אחד להזמנה');
      return;
    }
    const order:Order={
      SupplierId:this.selectedSupplierId,
      Status:'Pending',
      TotalPrice:this.orderDetails.reduce((sum,item)=>sum+(item.totalPrice||0),0),
      OrderDetails:this.orderDetails
    };
    this.generalService.add('Order',order).subscribe({
      next:()=>{
        alert('הזמנה נשלחה בהצלחה!');
        this.resetForm();
      },
      error:()=>{
        alert('שגיאה בשליחת הזמנה')
      }
    });
  }
  resetForm(): void {
    this.selectedSupplierId = null;
    this.products = [];
    this.selectedProductId = null;
    this.quantity = 1;
    this.orderDetails = [];
  }

}

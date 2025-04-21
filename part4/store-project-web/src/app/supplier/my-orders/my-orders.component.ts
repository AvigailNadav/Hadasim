import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth-service/auth.service';
import { GeneralService } from '../../services/general-service/general.service';
import { Order } from '../../models/Order.model';

@Component({
  selector: 'app-my-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.css'
})
export class MyOrdersComponent implements OnInit {
  orders: any[] = [];
  supplierId: number | null = null;
  loading: boolean = false;
  constructor(
    private generalService: GeneralService<Order>,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.supplierId = this.authService.getSupplierIdFromToken();

    if (!this.supplierId) {
      console.error('אין מזהה ספק בטוקן');
      this.router.navigate(['/login']);
      return;
    }
    this.generalService.getAll('order').subscribe({
      next: (data) => {
        this.orders = data.filter((order: any) => order.supplierId === this.supplierId);
      },
      error: (err) => {
        console.error('שגיאה בשליפת ההזמנות', err);
      }
    });
  }
  markAsInProgress(orderId: number): void {
    const newStatus = 'בתהליך';
    this.generalService.update('order', orderId, newStatus).subscribe({
      next: () => {
        const order = this.orders.find(o => o.id === orderId);
        if (order) order.status = newStatus;
      },
      error: (err) => {
        console.error('שגיאה בעדכון הסטטוס', err);
      }
    });
  }
}

import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Order } from '../../models/Order.model';
import { GeneralService } from '../../services/general-service/general.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit{
  orders:Order[]=[];

  constructor(private generalService:GeneralService<Order>){}

  ngOnInit(): void {
    this.generalService.getAll('Order').subscribe(data=>{
      this.orders=data;
    });
  }


}

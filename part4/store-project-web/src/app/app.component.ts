import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SupplierRegisterComponent } from './auth/register/supplier-register.component';
import { LoginComponent } from './auth/login/login.component';
import { SupplierDashboardComponent } from './supplier/supplier-dashboard/supplier-dashboard.component';
import { NavbarComponent } from './core/navbar/navbar.component';
import { HeaderComponent } from './core/header/header.component';
import { ManagerDashboardComponent } from './manager/manager-dashboard/manager-dashboard.component';
import { NewOrderComponent } from './manager/new-order/new-order.component';
import { OrdersComponent } from './manager/orders/orders.component';
import { ProductsComponent } from './manager/products/products.component';
import { SuppliersComponent } from './manager/suppliers/suppliers.component';
import { AddProductsComponent } from './supplier/add-products/add-products.component';
import { MyOrdersComponent } from './supplier/my-orders/my-orders.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    LoginComponent,
    SupplierRegisterComponent,
    HeaderComponent,
    NavbarComponent,
    ManagerDashboardComponent,
    NewOrderComponent,
    OrdersComponent,
    ProductsComponent,
    SuppliersComponent,
    AddProductsComponent,
    MyOrdersComponent,
    SupplierDashboardComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'store-project-web';
}

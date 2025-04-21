import { Routes } from '@angular/router';
import { SupplierRegisterComponent } from './auth/register/supplier-register.component';
import { RoleGuard } from './core/role.guard';
import { UserRole } from './models/user-role.enum';


export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./shared/home/home.component').then(m => m.HomeComponent)
    },
    {
        path: 'login',
        loadComponent: () => import('./auth/login/login.component')
            .then(m => m.LoginComponent)
    },
    {
        path: 'register',
        component: SupplierRegisterComponent
    },
    {
        path: 'manager',
        canActivate: [RoleGuard],
        data: { role: UserRole.Manager },
        children: [
            {
                path: 'products',
                loadComponent: () =>
                    import('./manager/products/products.component').then(m => m.ProductsComponent),
            },
            {
                path: 'supplier',
                loadComponent: () =>
                    import('./manager/suppliers/suppliers.component').then(m => m.SuppliersComponent),
            },
            {
                path: 'new-order',
                loadComponent: () =>
                    import('./manager/new-order/new-order.component').then(m => m.NewOrderComponent),
            },
            {
                path: 'orders',
                loadComponent: () =>
                    import('./manager/orders/orders.component').then(m => m.OrdersComponent),
            },
        ],
    },
    {
        path: 'supplier',
        canActivate: [RoleGuard],
        data: { role: UserRole.Supplier },
        children: [
            {
                path: 'add-product',
                loadComponent: () =>
                    import('./supplier/add-products/add-products.component').then(s => s.AddProductsComponent),
            },
            {
                path: 'my-orders',
                loadComponent: () =>
                    import('./supplier/my-orders/my-orders.component').then(s => s.MyOrdersComponent),
            },
            {
                path: 'dashboard',
                loadComponent: () =>
                    import('./supplier/supplier-dashboard/supplier-dashboard.component').then(s => s.SupplierDashboardComponent),
            }
        ]
    }
];

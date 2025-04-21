import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { suppliersProducts } from '../../models/Suppliers-products.model';
import { Supplier } from '../../models/Supplier.model';
import { Product } from '../../models/Product.model';

@Injectable({
  providedIn: 'root'
})
export class SupplierProductService {
  private apiUrl='https:localhost:7011/api/SupplierProduct'

  constructor(private http:HttpClient) { }

  getAllSupplierProducts():Observable<suppliersProducts[]>{
    return this.http.get<suppliersProducts[]>(this.apiUrl);
  }
  getSupplierProductById(id: number): Observable<suppliersProducts> {
    return this.http.get<suppliersProducts>(`${this.apiUrl}/id/${id}`);
  }
  getSuppliersByProductId(productId:number):Observable<Supplier[]>{
    return this.http.get<Supplier[]>(`${this.apiUrl}/product/${productId}/suppliers`);
  }
  getProductsBySupplierWithDetails(supplierId:number):Observable<Product[]>{
    return this.http.get<Product[]>(`${this.apiUrl}/supplier/${supplierId}/products/details`);
  }
  getProductsBySupplierId(supplierId:number):Observable<Product[]>{
    return this.http.get<Product[]>(`${this.apiUrl}/supplier/${supplierId}/products`);
  }
  addSupplierProduct(supplierProductDto:suppliersProducts):Observable<suppliersProducts>{
    return this.http.post<suppliersProducts>(`${this.apiUrl}`,supplierProductDto);
  }

}

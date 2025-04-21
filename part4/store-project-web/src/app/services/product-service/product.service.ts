import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../models/Product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl='https://localhost:7011/api/product'

  constructor(private http:HttpClient){}

  getAllProducts():Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }
  getProductById(id:number):Observable<Product>{
    return this.http.get<Product>(`${this.apiUrl}/id/${id}`);
  }
  getProductByName(name:string):Observable<Product>{
    return this.http.get<Product>(`${this.apiUrl}/name/${name}`);
  }
  addProduct(product:Product):Observable<Product>{
    return this.http.post<Product>(`${this.apiUrl}`,product);
  }
}

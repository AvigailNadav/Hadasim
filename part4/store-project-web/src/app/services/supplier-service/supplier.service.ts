import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Supplier } from '../../models/Supplier.model';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private apiUrl='https://localhost:7011/api/Supplier/'
  constructor(private http:HttpClient) { }

  getAllSuppliers():Observable<Supplier[]>{
    return this.http.get<Supplier[]>(`${this.apiUrl}getAllSuppliers`).
    pipe(
      catchError(error=>{
        console.error('שגיאה בקריאה לספקים',error);
        throw error;
      })
    );
  }
  getSupplierById(id:number):Observable<Supplier>{
    return this.http.get<Supplier>(`${this.apiUrl}id/${id}`).
    pipe(
      catchError(error=>{
        console.error('שגיאה בהבאת הספק',error);
        throw error;
      })
    );
  }
  getSupplierByName(name:string):Observable<Supplier>{
    return this.http.get<Supplier>(`${this.apiUrl}name/${name}`).
    pipe(
      catchError(error=>{
        console.error('שגיאה בהבאת הספק לפי השם',error);
        throw error;
      })
    );
  }
  registerSupplier(supplier:any):Observable<Supplier>{
    return this.http.post<Supplier>(`${this.apiUrl}registerSupplier`,supplier).
    pipe(
      catchError(error=>{
        console.error('שגיאה בהרשמת הספק',error);
        throw error;
      })

    );
  }
  logIn(credentials:{phoneNumber:string,password:string}):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}login`,credentials).
    pipe(
      catchError(error=>{
        console.error('שגיאה בהתחברות',error);
        throw error;
      })
    );
  }
}

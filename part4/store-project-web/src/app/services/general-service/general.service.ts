import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeneralService<T> {
  private apiUrl='https://localhost:7011/api';

  constructor(private http:HttpClient) { }
  
  getAll(endpoint:string):Observable<T[]>{
    return this.http.get<T[]>(`${this.apiUrl}/${endpoint}`);
  }
  getById(endpoint:string,id:number):Observable<T>{
    return this.http.get<T>(`${this.apiUrl}/${endpoint}/id/${id}`);
  }
  add(endpoint: string, entity: T): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${endpoint}`, entity);
  }
  update(endpoint: string, id: number, entity: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${endpoint}/${id}`, entity);
  }

}

import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../interfaces/Category';
import { CreateSubcategory } from '../interfaces/createsubcategory';
import { SubCategory } from '../interfaces/subcategory';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  GetProductsByUser(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'category/getcategory');
  }
}

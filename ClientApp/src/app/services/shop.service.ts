import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../interfaces/Category';
import { Food } from '../interfaces/food';
import { AddFood } from '../interfaces/add-food';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  GetProductsByUser(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'category/getcategory');
  }

  Update_Food(NewData: Food): Observable<Food> {
    return this.http.put<Food>(this.baseUrl + 'food/updatefood', NewData);
  }

  Add_food(NewData: AddFood): Observable<Food> {
    return this.http.post<Food>(this.baseUrl + 'food/addfood', NewData);
  }
}

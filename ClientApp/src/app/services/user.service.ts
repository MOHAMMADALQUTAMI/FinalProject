import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../interfaces/Category';
import { CreateSubcategory } from '../interfaces/createsubcategory';
import { SubCategory } from '../interfaces/subcategory';
import { Food } from '../interfaces/food';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  GetFoods(): Observable<Food[]> {
    return this.http.get<Food[]>(this.baseUrl + 'food/getfood');
  }

  GetOwnerFoods(): Observable<Food[]> {
    return this.http.get<Food[]>(this.baseUrl + 'food/getfoodbyiwner');
  }


}

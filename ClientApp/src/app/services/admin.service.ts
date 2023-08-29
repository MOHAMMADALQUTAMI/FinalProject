import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../interfaces/Category';
import { CreateSubcategory } from '../interfaces/createsubcategory';
import { SubCategory } from '../interfaces/subcategory';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'category/getcategory');
  }

  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.baseUrl + 'category/post', category);
  }

  getSubCategories(): Observable<SubCategory[]> {
    return this.http.get<SubCategory[]>(this.baseUrl + 'subcategory/getsubcategories');
  }

  addSubCategory(subcategory: CreateSubcategory): Observable<SubCategory> {
    return this.http.post<SubCategory>(this.baseUrl + 'subcategory/post', subcategory);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TestService {
  private apiUrl = '/api/Category'; // Replace with your API endpoint URL

  constructor(private http: HttpClient) { }

  getCategories(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetCategory`);
  }
}
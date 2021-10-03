import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../model/product';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  private readonly url = 'https://schoppingcart.azurewebsites.net/api/Products';

  loadProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.url);
  }
}

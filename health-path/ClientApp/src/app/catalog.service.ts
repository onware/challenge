import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { NaturalProduct } from './model/catalog.model';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private readonly catalogUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.catalogUrl = baseUrl + "api/catalog";
  }

  getProducts() {
    return this.http.get(this.catalogUrl, { "observe": "body", "responseType": "json" }).pipe(map(x => x as readonly NaturalProduct[]));
  }
}

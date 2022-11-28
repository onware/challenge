import { Component, OnInit } from '@angular/core';
import { CatalogService } from '../catalog.service';
import { NaturalProduct } from '../model/catalog.model';

@Component({
  selector: 'app-store-catalog',
  templateUrl: './store-catalog.component.html',
  styleUrls: ['./store-catalog.component.css']
})
export class StoreCatalogComponent implements OnInit {

  public products: readonly NaturalProduct[] = [];
  public readonly columnsToDisplay = ['licenceNo', 'productName', 'companyName', 'active', 'purposes'];

  constructor(private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.catalogService.getProducts().subscribe({
      next: x => this.products = x
    });
  }
}

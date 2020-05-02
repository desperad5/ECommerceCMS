import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class ProductCategoryService {
	private headerOptions: HttpHeaders;
	headers = new HttpHeaders()
		.set('Content-Type', 'application/json')
		.set('Accept', 'application/json');

	constructor(private http: HttpClient) { }

	fetchAllActiveProductCategories() {
		return this.http.post("/api/ProductCategory/FetchAllActiveProductCategories", {}, { headers: this.headers })
	};
	fetchAllListings() {
		return this.http.post("/api/ProductCategory/FetchAllActiveListings", {}, { headers: this.headers })
	}

	createOrEditProductCategory(productCategory: any) {
		return this.http.post<any>("/api/ProductCategory/CreateOrEdit", productCategory, { headers: this.headers });
	};

	deleteProductCategoryById(id: number) {
		return this.http.post("/api/ProductCategory/DeleteProductCategoryById", id, { headers: this.headers });
	};

	fetchAllProductCategories() {
		return this.http.post("/api/ProductCategory/fetchAllProductCategories", {}, { headers: this.headers });
	}
}

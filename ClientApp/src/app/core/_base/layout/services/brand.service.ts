import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class BrandService {
	private headerOptions: HttpHeaders;
	headers = new HttpHeaders()
		.set('Content-Type', 'application/json')
		.set('Accept', 'application/json');

	constructor(private http: HttpClient) { }

	fetchAllActiveBrands() {
		return this.http.post("/api/Brand/FetchAllActiveBrands", {}, { headers: this.headers })
	};

	createOrEditBrand(topic: any) {
		return this.http.post<any>("/api/Brand/CreateOrEdit", topic, { headers: this.headers });
	};

	deleteBrandById(id: number) {
		return this.http.post("/api/Brand/DeleteTopicById", id, { headers: this.headers });
	};

	downloadExcel() {
		return this.http.post<Blob>("/api/Excel/DownloadExcel", {}, { headers: this.headers, responseType: 'blob' as 'json' });
	}

}

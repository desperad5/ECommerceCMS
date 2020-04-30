import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class BundleService {
    private headerOptions: HttpHeaders;
    headers = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('Accept', 'application/json');

    constructor(private http: HttpClient) {
    }

    fetchAllQuestionCards() {
        return this.http.post("/api/Card/FetchAllQuestionCards", {}, { headers: this.headers })
    };

    fetchAllBundles() {
        return this.http.post("/api/Bundle/FetchAllBundles", {}, { headers: this.headers })
    };

    createOrEditBundle(data: any) {
        return this.http.post("/api/Bundle/CreateOrEditBundle", data, { headers: this.headers })
    };

    deleteBundleById(id: number) {
        return this.http.post<any>("/api/Bundle/DeleteBundleById", id, { headers: this.headers })
    };



}



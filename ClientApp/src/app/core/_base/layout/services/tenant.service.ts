import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class TenantService {
    private headerOptions: HttpHeaders;
    headers = new HttpHeaders()
    .set('Content-Type', 'application/json')
    .set('Accept', 'application/json');

    constructor(private http: HttpClient) {
    }

    fetchAllTenants() {
        return this.http.post("/api/Tenant/FetchAllTenants", {}, { headers: this.headers })
    };

    createOrEdit(tenant: any) {
        return this.http.post<any>("/api/Tenant/CreateOrEdit", tenant, { headers: this.headers });
    };

    DeleteTenantById(id: number) {
        return this.http.post<any>("/api/Tenant/DeleteTenantById", id, { headers: this.headers });
    };

}



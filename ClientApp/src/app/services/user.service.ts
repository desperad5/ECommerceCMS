import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class UserService {
    private headerOptions: HttpHeaders;
    headers = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .set('Accept', 'application/json');

    constructor(private http: HttpClient) {
    }

    fetchAllUsers() {
        return this.http.post("/api/Auth/FetchAllUsers", {}, { headers: this.headers })
    };

    createOrEdit(user: any) {
        return this.http.post<any>("/api/Auth/CreateOrEdit", user, { headers: this.headers });
    };

    deleteUserById(id: number) {
        return this.http.post<any>("/api/Auth/DeleteUserById", id, { headers: this.headers });
    };

    changePassword(user: any) {
        return this.http.post<any>("/api/Auth/ChangePassword", user, { headers: this.headers });
    };

}



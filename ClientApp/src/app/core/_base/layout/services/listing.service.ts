import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';


@Injectable({
	providedIn: 'root'
})
export class ListingService {
    private headerOptions: HttpHeaders;
    headers = new HttpHeaders()
    .set('Content-Type', 'application/json')
    .set('Accept', 'application/json');

	constructor(private http: HttpClient, private router: Router) {
    }

    fetchAllListings() {
		return this.http.post("/api/ProductCategory/FetchAllActiveListings", {}, { headers: this.headers }).pipe(
			catchError(this.handleError)
		);
    };

    createOrEdit(listing: any) {
		return this.http.post<any>("/api/Listing/CreateOrEdit", listing, { headers: this.headers }).pipe(
			catchError(this.handleError)
		);;
    };

    DeleteListingById(id: number) {
		return this.http.post<any>("/api/Listing/DeleteListingById", id, { headers: this.headers }).pipe(
			catchError(this.handleError)
		);;
	};
	private handleError(error: HttpErrorResponse) {
		debugger;
		if (error.status == 401) {
			debugger;
			localStorage.removeItem('authToken');
			localStorage.removeItem('email');
			localStorage.removeItem('isAdmin');
			this.router.navigate(['/auth/login']);
		}
		if (error.error instanceof ErrorEvent) {
			// A client-side or network error occurred. Handle it accordingly.
			console.error('An error occurred:', error.error.message);
		} else {
			// The backend returned an unsuccessful response code.
			// The response body may contain clues as to what went wrong,
			console.error(
				`Backend returned code ${error.status}, ` +
				`body was: ${error.error}`);
		}
		// return an observable with a user-facing error message
		return throwError(
			'Something bad happened; please try again later.');
	};

}

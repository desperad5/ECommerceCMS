// Angular
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
// RxJS
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';


/**
 * More information there => https://medium.com/@MetonymyQT/angular-http-interceptors-what-are-they-and-how-to-use-them-52e060321088
 */
@Injectable()
export class InterceptService implements HttpInterceptor {
	constructor(private router: Router) {

	}
	intercept(
		request: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		
		const authToken = localStorage.getItem("authToken");
		if (authToken) {
			
			const cloned = request.clone({
				headers: request.headers.set("Authorization",
					"Bearer " + authToken)
			});

			return next.handle(cloned);
		}

		return next.handle(request).pipe(
			tap(
				event => {
					if (event instanceof HttpResponse) {
						
						if (event.status === 401) {
							
							localStorage.removeItem('authToken');
							localStorage.removeItem('email');
							localStorage.removeItem('isAdmin');
							this.router.navigate(['/auth/login']);
						}
						// console.log('all looks good');
						// http response status code
						// console.log(event.status);
					}
				},
				error => {
					
					if (error.status === 401) {
						localStorage.removeItem('authToken');
						localStorage.removeItem('email');
						localStorage.removeItem('isAdmin');
						this.router.navigate(['/auth/login']);
					}
					// http response status code
					// console.log('----response----');
					// console.error('status code:');
					// tslint:disable-next-line:no-debugger
					console.error(error.status);
					console.error(error.message);
					// console.log('--- end of response---');
				}
			)
		);
	}
}

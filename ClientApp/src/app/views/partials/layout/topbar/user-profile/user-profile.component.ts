// Angular
import { Component, Input, OnInit } from '@angular/core';
// RxJS
import { Observable } from 'rxjs';
// NGRX
import { select, Store } from '@ngrx/store';
// State
import { AppState } from '../../../../../core/reducers';
import { currentUser, Logout, User } from '../../../../../core/auth';
import { take } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'kt-user-profile',
	templateUrl: './user-profile.component.html',
})
export class UserProfileComponent implements OnInit {
	// Public properties
	email = localStorage.getItem('email');
	picUrl = localStorage.getItem('picUrl');
	user$: Observable<User>;
	_user: User;

	@Input() avatar = true;
	@Input() greeting = true;
	@Input() badge: boolean;
	@Input() icon: boolean;

	/**
	 * Component constructor
	 *
	 * @param store: Store<AppState>
	 */
	constructor(private store: Store<AppState>, public translate: TranslateService) {

	}

	/**
	 * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
	 */

	/**
	 * On init
	 */
	ngOnInit(): void {
		this.user$ = new Observable<User>();
		this._user = new User();
		this._user.fullname = "admin";
		this._user.pic = "picture.jpg";
		this._user.id = 6;

	}

	/**
	 * Log out
	 */
	logout() {
		localStorage.removeItem('authToken');
		localStorage.removeItem('email');
		localStorage.removeItem('isAdmin');
		this.store.dispatch(new Logout());
	}
}

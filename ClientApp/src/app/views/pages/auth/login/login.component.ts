// Angular
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';


import { AccountService } from '../../../../core/services/account.service';
import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
import { AuthNoticeService, AuthService, Login } from '../../../../core/auth';
import { Observable } from 'rxjs';


@Component({
	selector: 'kt-login',
	templateUrl: './login.component.html',
	encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {


	loginForm: FormGroup;
	userData: any[] = [];
	errorMessage: string;

	emailFormControl = new FormControl('', [
		Validators.required,
		Validators.email,
	]);

	constructor(private fb: FormBuilder,
		private accountService: AccountService,
		private router: Router,
		public translate: TranslateService,
		private store: Store<AppState>,
		private authNoticeService: AuthNoticeService,
		private cdr: ChangeDetectorRef

	) { }


	ngOnInit(): void {
		this.initLoginForm();
	}

	initLoginForm() {
		this.loginForm = this.fb.group({
			email: ['', Validators.compose([
				Validators.required,
				Validators.email,
				Validators.minLength(3),
				Validators.maxLength(320)
			])],
			password: ['', Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])]
		});
	}

	loginWithEmail() {
		const controls = this.loginForm.controls;

		if (this.loginForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			return;
		};
		this.accountService.loginWithEmail(this.loginForm.value.email, this.loginForm.value.password)
			.subscribe((response) => {
				 
				console.log(response);
				localStorage.setItem("email", this.loginForm.value.email);
				localStorage.setItem("authToken", response.token);
				localStorage.setItem("isAdmin", response.isAdmin);
				//this.store.dispatch(new Login({authToken:response.token}));
				//this.authNoticeService.setNotice(null);
				//this.cdr.markForCheck();
				// setTimeout(() => {
				// 	this.router.navigate(['/dashboard']);
				// }, 4000);
				window.location.href = "/dashboard";
			},
				error => {
					console.log(error);
					 
					let messageCode = error.error;
					this.errorMessage = this.translate.instant(messageCode);
				});
	}

	public message = false;
	sendRegistrationMail() {
		this.message = true;
		this.accountService.sendRegistrationMail(this.emailFormControl.value)
			.subscribe((response) => {
				console.log(response);
			},
				error => {

					console.log(error);
					this.errorMessage = error.error;
				});

	}

	submit() {
		const controls = this.loginForm.controls;

		if (this.loginForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			return;
		}
		const authData = {
			email: controls.email.value,
			password: controls.password.value
		};

	}

	isControlHasError(controlName: string, validationType: string): boolean {
		const control = this.loginForm.controls[controlName];
		if (!control) {
			return false;
		}

		const result = control.hasError(validationType) && (control.dirty || control.touched);
		return result;
	}
}

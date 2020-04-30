// Angular
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// RxJS
import { finalize, takeUntil, tap } from 'rxjs/operators';
// Translate
import { TranslateService } from '@ngx-translate/core';
// NGRX
import { Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
// Auth
import { AuthNoticeService, AuthService, Register, User } from '../../../../core/auth/';
import { Subject, Subscription } from 'rxjs';
import { ConfirmPasswordValidator } from './confirm-password.validator';
import { AccountService } from '../../../../core/services/account.service';
import { MatSnackBar } from '@angular/material';

@Component({
	selector: 'kt-change-password-with-mail',
	templateUrl: './change-password-with-mail.component.html'
})
export class ChangePasswordWithMailComponent implements OnInit, OnDestroy {
	registerForm: FormGroup;
	loading = false;
	errors: any = [];
	queryparamSubscription: Subscription;
	passwordChangeCode = "";


	constructor(
		private router: Router,
		protected route: ActivatedRoute,
		private fb: FormBuilder,
		private accountService: AccountService,
		public snackBar: MatSnackBar,
		private translate:TranslateService
	) {
	}

	ngOnInit() {
		this.initRegisterForm();
		this.queryparamSubscription = this.route.queryParams
			.subscribe(params => {
				if (params && params.code && params.email) {
					console.log("params:" + params);
					this.passwordChangeCode = params.code;
					this.registerForm.controls['email'].setValue(params.email);
				}
			});
	}

	ngOnDestroy(): void {
		this.loading = false;
	}

	initRegisterForm() {
		this.registerForm = this.fb.group({
			email: [{ value: '', disabled: true }, Validators.compose([
				Validators.required,
				Validators.email,
				Validators.minLength(3),
				Validators.maxLength(320)
			]),
			],
			password: ['', Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])
			],
			confirmPassword: ['', Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])
			]
		}, {
			validator: ConfirmPasswordValidator.MatchPassword
		});

	}

    message="";

	submit() {
		const controls = this.registerForm.controls;
        this.message="";
		if (this.registerForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			return;
		}

		var postData = {
			"EmailAddress": this.registerForm.get('email').value,
			"Password": this.registerForm.get('password').value,//new password
			"Code": this.passwordChangeCode
		};

		this.accountService.changePasswordWithCode(postData).subscribe(
			(response) => {
				console.log(response);
				this.router.navigate(['/auth/login']);
				this.snackBar.open(this.translate.instant("PASSWORD_CHANGE_SUCCESS"), this.translate.instant("CLOSE"), {
					duration: 4000,
				});

			},
			error => {
				console.log(error);
				 
				this.message=error.error;
			}
		);

	}


	isControlHasError(controlName: string, validationType: string): boolean {
		const control = this.registerForm.controls[controlName];
		if (!control) {
			return false;
		}

		const result = control.hasError(validationType) && (control.dirty || control.touched);
		return result;
	}
}

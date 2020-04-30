// Angular
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
// RxJS
import { finalize, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
// Translate
import { TranslateService } from '@ngx-translate/core';
// Auth
import { AuthNoticeService, AuthService } from '../../../../core/auth';
import { AccountService } from '../../../../core/services/account.service';

@Component({
	selector: 'kt-forgot-password',
	templateUrl: './forgot-password.component.html'
})
export class ForgotPasswordComponent implements OnInit {

emailFormControl = new FormControl('', [
		Validators.required,
		Validators.email,
	  ]);
	  loading = false;

	errors: any = [];
    message:string;
	private unsubscribe: Subject<any>; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/


	constructor(
		private authService: AuthService,
		public authNoticeService: AuthNoticeService,
		private translate: TranslateService,
		private router: Router,
		private fb: FormBuilder,
		private cdr: ChangeDetectorRef,
		private accountService: AccountService,
	) {
		this.unsubscribe = new Subject();
	}

	ngOnInit() {
	}


	sendForgotPasswordMail() {
		const controls = this.emailFormControl;

		if (this.emailFormControl.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			return;
		};

		this.accountService.forgotPasswordSendEmail(this.emailFormControl.value)
		  .subscribe((response) => {
			console.log(response);
			this.message="The code sent."
		  },
			error => {
				this.message=error.error;
			  console.log(error);
			});
	
	  }

	isControlHasError(controlName: string, validationType: string): boolean {
		const control = this.emailFormControl;
		if (!control) {
			return false;
		}

		const result =
			control.hasError(validationType) &&
			(control.dirty || control.touched);
		return result;
	}


}

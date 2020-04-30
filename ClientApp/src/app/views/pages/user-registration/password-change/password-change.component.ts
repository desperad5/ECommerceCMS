import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService } from '../../../../services/user.service';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmPasswordValidator } from '../../auth/register/confirm-password.validator';

@Component({
	selector: 'password-change',
	templateUrl: './password-change.component.html'
})
export class PasswordChangeComponent implements OnInit {

	user = {
		id: 0,
		userName: "",
		name: "",
		surname: "",
		registrationNumber: "",
		emailAddress: "",
		password: "",
		phone: "",
		pictureUrl: "",
		languagePreference: "tr",
		confirmPassword: "",
		tenantId: 0
	};

	errors: any = [];

	passwordForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;
	tenants = [];
	constructor(public dialogRef: MatDialogRef<PasswordChangeComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private userService: UserService,
		public translate: TranslateService
	) {

	}

	ngOnInit() {
		if (this.data.user)
		this.user = this.data.user;
		this.initForm();
	}

	isControlHasError(controlName: string, validationType: string): boolean {
		const control = this.passwordForm.controls[controlName];
		if (!control) {
			return false;
		}
		const result = control.hasError(validationType) && (control.dirty || control.touched);
		return result;
	}

	initForm() {

		this.passwordForm = this.fb.group({
			id: [this.user.id],		
			password: [this.user.password, Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])],
			confirmPassword: [this.user.confirmPassword, Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])],
		},
		 {
			validator: ConfirmPasswordValidator.MatchPassword
		});
	}


	dialogClose(): void {
		this.dialogRef.close();
	}

	onSubmit() {

		this.hasFormErrors = false;
		const controls = this.passwordForm.controls;
		 
		if (this.passwordForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			this.userService.changePassword(this.passwordForm.value).subscribe(
				data => {
					this.dialogRef.close({ user: data, isEdit: false });
				},
				error => console.log('oops', error)
			);
		}

	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

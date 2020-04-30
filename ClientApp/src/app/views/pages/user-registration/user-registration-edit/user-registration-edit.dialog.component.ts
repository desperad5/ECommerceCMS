import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UserService } from '../../../../services/user.service';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmPasswordValidator } from '../../auth/register/confirm-password.validator';
import { TenantService } from '../../../../core/_base/layout';

@Component({
	selector: 'kt-user-registration-edit.dialog.component',
	templateUrl: './user-registration-edit.dialog.component.html'
})
export class UserRegistrationEditDialogComponent implements OnInit {

	user = {
		id: 0,
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

	userForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;
	tenants = [];
	constructor(public dialogRef: MatDialogRef<UserRegistrationEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private userService: UserService,
		public translate: TranslateService,
		private tenantService: TenantService
	) {

	}

	ngOnInit() {
		this.fetchAllTenants();
		if (this.data.userModel)
			this.user = this.data.userModel;
		this.initRegisterForm();
	}

	fetchAllTenants() {
		this.tenantService.fetchAllTenants().subscribe((data: any) => {
			 
			this.tenants = data;
		});
	}

	isControlHasError(controlName: string, validationType: string): boolean {
		const control = this.userForm.controls[controlName];
		if (!control) {
			return false;
		}
		const result = control.hasError(validationType) && (control.dirty || control.touched);
		return result;
	}

	initRegisterForm() {
		 
		let passwordValidation = this.data.userModel!=undefined ? Validators.compose([]) : Validators.compose([
			Validators.required,
			Validators.minLength(8),
			Validators.maxLength(20)
		]);

		let passwordMatchValidator = this.data.userModel!=undefined ? {} : {
			validator: ConfirmPasswordValidator.MatchPassword
		};

		this.userForm = this.fb.group({
			id: [this.user.id],
			surname: [this.user.surname, Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])],
			name: [this.user.name, Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])],
			emailAddress: [this.user.emailAddress, Validators.compose([
				Validators.required,
				Validators.email,
				Validators.minLength(3),
				Validators.maxLength(320)
			])],
			
			password: [this.user.password, passwordValidation],
			confirmPassword: [this.user.confirmPassword, passwordValidation],
			tenantId: [this.user.tenantId, Validators.compose([
				Validators.required
			])]

		}, passwordMatchValidator);

	}



	getTitle(): string {
		if (this.user.id > 0) {
			return this.translate.instant("EDIT_USER");
		}
		return this.translate.instant("ADD_USER");
	}

	dialogClose(): void {
		this.dialogRef.close();
	}

	onSubmit() {
		 
		this.hasFormErrors = false;
		const controls = this.userForm.controls;
		 
		if (this.userForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			this.userService.createOrEdit(this.userForm.value).subscribe(
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

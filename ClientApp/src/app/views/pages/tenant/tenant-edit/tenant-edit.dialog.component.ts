// Angular
import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// Material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TenantService } from '../../../../core/_base/layout';
import { TranslateService } from '@ngx-translate/core';

@Component({
	// tslint:disable-next-line:component-selector
	selector: 'kt-tenants-edit-dialog',
	templateUrl: './tenant-edit.dialog.component.html'
})
export class TenantEditDialogComponent implements OnInit, OnDestroy {
	tenant = {
		id: 0,
		name: "",
		address: "",
		county: "",
		town: "",
		typeId: "",
		phoneNumber: "",
		taxAdministration: "",
		taxNumber: "",
		actions: ""
	};

	tenantForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;
	tenantTypes = [{
		id: 0, name: this.translate.instant("PRIVATE_SECTOR")
	}, {
		id: 1, name: this.translate.instant("PUBLIC_SECTOR")
	}];
	constructor(public dialogRef: MatDialogRef<TenantEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private tenantService: TenantService,
		public translate: TranslateService

	) {
	}


	ngOnInit() {
		 
		console.log("Createtenant:" + this.data.tenantModel);
		if (this.data.tenantModel)
			this.tenant = this.data.tenantModel;

		this.createForm();

	}

	ngOnDestroy() {

	}
	createForm() {
		this.tenantForm = this.fb.group({
			id: [this.tenant.id],
			name: [this.tenant.name, Validators.required],
			createdDate: [new Date()],
			address: [this.tenant.address, Validators.minLength(5)],
			typeId: [this.tenant.typeId],
			phoneNumber: [this.tenant.phoneNumber, [Validators.pattern("[0-9 ]{11}")]],
			taxAdministration: [this.tenant.taxAdministration],
			taxNumber: [this.tenant.taxNumber],
		});
	}

	getTitle(): string {
		if (this.tenant.id > 0) {
			return this.translate.instant("EDIT_TENANT");
		}
		return this.translate.instant("ADD_TENANT");
	}
	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.tenantForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}


	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.tenantForm.controls;
		 
		if (this.tenantForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			this.tenantForm.value.typeId = Number(this.tenantForm.value.typeId);
			this.tenantService.createOrEdit(this.tenantForm.value).subscribe(
				data => {   console.log('success', data); this.dialogRef.close({ tenant: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}

	}

	/** Alect Close event */
	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

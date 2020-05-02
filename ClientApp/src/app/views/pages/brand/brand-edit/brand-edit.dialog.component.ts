import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { BrandService } from '../../../../core/_base/layout/services/brand.service';

@Component({
	selector: 'brand-edit.dialog',
	templateUrl: './brand-edit.dialog.component.html',
	styleUrls: ['./brand-edit.dialog.component.scss']
})
export class BrandEditDialogComponent implements OnInit {

	brand = { id: 0, name: '', webSiteUrl:'' };
	brands;

	brandForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	constructor(
		public dialogRef: MatDialogRef<BrandEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private brandService: BrandService,
		public translate: TranslateService
	) { }

	ngOnInit() {

		if (this.data.brandModel) {
			this.brand = this.data.brandModel;
		}
		this.brandService.fetchAllActiveBrands().subscribe((data: any) => {
			this.brands = data;
		});
		this.createForm();
	}

	createForm() {
		this.brandForm = this.fb.group({
			id: [this.brand.id],
			name: [this.brand.name, Validators.required],
			webSiteUrl: [this.brand.webSiteUrl]
		});
	}

	getTitle(): string {
		if (this.brand.id > 0) {
			return this.translate.instant("BRAND.EDIT_BRAND");
		}
		return this.translate.instant("BRAND.NEW_BRAND");
	}

	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.brandForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.brandForm.controls;
		if (this.brandForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			
			this.brandService.createOrEditBrand(this.brandForm.value).subscribe(
				data => { console.log('success', data); this.dialogRef.close({ brand: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { ProductCategoryService } from '../../../../core/_base/layout/services/productcategory.service';

@Component({
	selector: 'kt-productcategory-edit.dialog',
	templateUrl: './productcategory-edit.dialog.component.html',
	styleUrls: ['./productcategory-edit.dialog.component.scss']
})
export class ProductCategoryEditDialogComponent implements OnInit {

	productCategory = { id: 0, categoryName: '',listingId:0, parentCategoryId: 0 };
	listings;
	productCategories;
	listingCategories;

	categoryForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	constructor(
		public dialogRef: MatDialogRef<ProductCategoryEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private productCategoryService: ProductCategoryService,
		public translate: TranslateService
	) { }

	ngOnInit() {
		 
		if (this.data.productCategoryModel) {
			this.productCategory = this.data.productCategoryModel;
		}
		this.productCategoryService.fetchAllListings().subscribe((data: any) => {
			this.listings = data;
		});
		this.productCategoryService.fetchAllActiveProductCategories().subscribe((data: any) => {
			this.productCategories = data;
			this.calculateCategoryForListing();
		});
		this.createForm();
	}

	createForm() {
		this.categoryForm = this.fb.group({
			id: [this.productCategory.id],
			categoryName: [this.productCategory.categoryName, Validators.required],
			listingId: [this.productCategory.listingId, Validators.compose([Validators.required,Validators.min(1)])],
			parentCategoryId:[this.productCategory.parentCategoryId]
		});
	}

	calculateCategoryForListing() {
		var listingId = this.categoryForm.value.listingId;
		this.listingCategories = new Array<any>();
		if (this.productCategories) {
			for (let category of this.productCategories) {
				if (category.listingId == listingId) {
					this.listingCategories.push(category);
				}
			}
		}
	}

	listingChangeListener($event) {
		this.calculateCategoryForListing();
	}

	getTitle(): string {
		if (this.productCategory.id > 0) {
			return this.translate.instant("PRODUCTCATEGORY.EDIT_PRODUCTCATEGORY");
		}
		return this.translate.instant("PRODUCTCATEGORY.NEW_PRODUCTCATEGORY");
	}

	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.categoryForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.categoryForm.controls;
		if (this.categoryForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			debugger;
			this.categoryForm.value.listingId = Number(this.categoryForm.value.listingId);
			this.categoryForm.value.parentCategoryId = Number(this.categoryForm.value.parentCategoryId);
			console.log(this.categoryForm.value);
			this.productCategoryService.createOrEditProductCategory(this.categoryForm.value).subscribe(
				data => { console.log('success', data); this.dialogRef.close({ category: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

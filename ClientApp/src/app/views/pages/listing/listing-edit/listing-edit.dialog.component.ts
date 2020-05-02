import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { ListingService } from '../../../../core/_base/layout/services/listing.service';

@Component({
	selector: 'kt-listing-edit.dialog',
	templateUrl: './listing-edit.dialog.component.html',
	styleUrls: ['./listing-edit.dialog.component.scss']
})
export class ListingEditDialogComponent implements OnInit {

	listing = { id: 0, name: '',description:'' };
	listings;


	listingForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	constructor(
		public dialogRef: MatDialogRef<ListingEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private listingService: ListingService,
		public translate: TranslateService
	) { }

	ngOnInit() {
		 
		if (this.data.listingCategoryModel) {
			this.listing = this.data.listingCategoryModel;
		}
		this.createForm();
	}

	createForm() {
		this.listingForm = this.fb.group({
			id: [this.listing.id],
			name: [this.listing.name, Validators.required],
			description: [this.listing.description]
		});
	}

	

	

	getTitle(): string {
		if (this.listing.id > 0) {
			return this.translate.instant("LISTING.EDIT_LISTING");
		}
		return this.translate.instant("LISTING.NEW_LISTING");
	}

	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.listingForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.listingForm.controls;
		if (this.listingForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			debugger;
			this.listingForm.value.listingId = Number(this.listingForm.value.listingId);
			console.log(this.listingForm.value);
			this.listingService.createOrEdit(this.listingForm.value).subscribe(
				data => { console.log('success', data); this.dialogRef.close({ listing: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

// Angular
import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// Material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LessonService } from '../../../../core/_base/layout';
import { TranslateService } from '@ngx-translate/core';

@Component({
	// tslint:disable-next-line:component-selector
	selector: 'kt-lessons-edit-dialog',
	templateUrl: './lesson-edit.dialog.component.html'
})
export class LessonEditDialogComponent implements OnInit, OnDestroy {
	lesson = {
		id: 0,
		name: "",
		educationLevel: 0,
		educationLevelString: "",
		isActive: true,
		isActiveString: "",
		topics: undefined,
		actions: ""
	};

	educationLevelToString = ["ilkokul","lise","üniversite"];

	lessonForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	constructor(public dialogRef: MatDialogRef<LessonEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private lessonService: LessonService,
		public translate: TranslateService

	) {
	}


	ngOnInit() {
		console.log("Createlesson:" + this.data.lessonModel);
		if (this.data.lessonModel){
			this.lesson = this.data.lessonModel;
			this.lesson.educationLevelString = this.data.lessonModel.educationLevel.toString();
			this.lesson.isActiveString = this.data.lessonModel.isActive ? "1" : "0";
		}
			
		this.createForm();
	}

	ngOnDestroy() {

	}
	createForm() {
		
		this.lessonForm = this.fb.group({
			id: [this.lesson.id],
			name: [this.lesson.name, Validators.required],
			educationLevel: [this.lesson.educationLevel, Validators.required],
			isActive: [this.lesson.isActive, Validators.required],
			createdDate: [new Date(), Validators.compose([Validators.nullValidator])],
			educationLevelString : [this.lesson.educationLevelString],
			isActiveString : [this.lesson.isActiveString]
		});
	}

	getTitle(): string {
		if (this.lesson.id > 0) {
			return this.translate.instant("EDIT_LESSON");
		}
		return this.translate.instant("ADD_LESSON");
	}
	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.lessonForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}


	onSubmit() {
		this.hasFormErrors = false;
		this.lessonForm.value.educationLevel = Number(this.lessonForm.value.educationLevelString);
		this.lessonForm.value.isActive = ("1" == this.lessonForm.value.isActiveString);
		const controls = this.lessonForm.controls;
		 
		if (this.lessonForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			this.lessonService.createOrEdit(this.lessonForm.value).subscribe(
				data => {  console.log('success', data); this.dialogRef.close({ lesson: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
		

	}

	updateCustomer(_lesson: any) {

		//this.store.dispatch(new CustomerUpdated({
		//	partialCustomer: updateCustomer,
		//	customer: _customer
		//}));

		// Remove this line
		// Uncomment this line
		// this.dialogRef.close({ _customer, isEdit: true }
	}

	/**
	 * Create customer
	 *
	 * @param _customer: CustomerModel
	 */
	createCustomer(_lesson: any) {
		//this.store.dispatch(new CustomerOnServerCreated({ customer: _customer }));
		//this.componentSubscriptions = this.store.pipe(
		//	select(selectLastCreatedCustomerId),
		//	delay(1000), // Remove this line
		//).subscribe(res => {
		//	if (!res) {
		//		return;
		//	}

		//	this.dialogRef.close({ _customer, isEdit: false });
		//});
	}

	/** Alect Close event */
	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

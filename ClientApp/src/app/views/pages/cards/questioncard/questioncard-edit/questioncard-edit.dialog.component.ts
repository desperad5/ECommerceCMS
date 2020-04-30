// Angular
import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// Material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { QuestionCardService } from '../../../../../core/_base/layout/services/questioncard.service';
import { TranslateService } from '@ngx-translate/core';
import { LessonService } from '../../../../../core/_base/layout/services/lesson.service';
import { TopicService } from '../../../../../core/_base/layout/services/topic.service';
import { TenantService } from '../../../../../core/_base/layout/services/tenant.service';

@Component({
	selector: 'kt-questioncards-edit-dialog',
	templateUrl: './questioncard-edit.dialog.component.html'
})
export class
	QuestionCardEditDialogComponent implements OnInit, OnDestroy {
	questioncard = {
		id: 0,
		name: "",
		createdData: Date,
		description: "",
		tenantId: 0,
		isActive: true,
		lessonId: 0,
		questionCount: undefined,
		fileUrl: "",
		price: undefined,
		topicId: 0,
		actions: ""
	};

	questioncardForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	allTenants: any;
	allLessons: any;
	allTopics: any;

	topicsToShow = new Array<any>();


	constructor(public dialogRef: MatDialogRef<QuestionCardEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private questioncardService: QuestionCardService,
		public translate: TranslateService,
		private tenantService: TenantService,
		private lessonService: LessonService,
		private topicService: TopicService
	) {
	}


	ngOnInit() {
		console.log("Createquestioncard:" + this.data.questioncardModel);
		if (this.data.questioncardModel) {
			this.questioncard = this.data.questioncardModel;
			this.questioncard.tenantId = this.data.questioncardModel.tenant.id;
			this.questioncard.lessonId = this.data.questioncardModel.lesson.id;
			this.questioncard.topicId = this.data.questioncardModel.topic.id;
		}

		this.createForm();
		this.getAllResources();
	}

	calculateTopicsForLesson() {
		var lessonId = this.questioncardForm.value.lessonId;
		this.topicsToShow = new Array<any>();
		if (this.allTopics) {
			for (let topic of this.allTopics) {
				if (lessonId == topic.lessonId) {
					this.topicsToShow.push(topic);
				}
			}
		}
	}

	getAllResources() {
		this.tenantService.fetchAllTenants().subscribe((data: any) => {
			this.allTenants = data;
		});
		this.lessonService.fetchAllLessons().subscribe((data: any) => {
			this.allLessons = data;
		});
		this.topicService.fetchAllActiveTopics().subscribe((data: any) => {
			this.allTopics = data;
			this.calculateTopicsForLesson();
		});
	}

	ngOnDestroy() {

	}

	createForm() {

		this.questioncardForm = this.fb.group({
			id: [this.questioncard.id],
			name: [this.questioncard.name, Validators.required],
			description: [this.questioncard.description],
			tenantId: [this.questioncard.tenantId, Validators.required],
			lessonId: [this.questioncard.lessonId, Validators.required],
			questionCount: [this.questioncard.questionCount, Validators.compose([Validators.min(1), Validators.max(100000),
			Validators.required, Validators.pattern(/^-?(0|[1-9]\d*)?$/)])],
			fileUrl: [this.questioncard.fileUrl, Validators.required],
			price: [this.questioncard.price, Validators.required],
			topicId: [this.questioncard.topicId, Validators.required]
			//isActive: [this.questioncard.isActive, Validators.required],
		});
	}

	fileUploadFinished(input) {
		this.questioncardForm.value.fileUrl = "https://localhost:5486/Upload/" + input.data.name;
		this.questioncardForm.controls["fileUrl"].setValue(this.questioncardForm.value.fileUrl);
	}

	lessonChangeListener($event) {
		this.calculateTopicsForLesson();
	}

	getTitle(): string {
		if (this.questioncard.id > 0) {
			return this.translate.instant("QUESTIONCARD.EDIT_QUESTIONCARD");
		}
		return this.translate.instant("QUESTIONCARD.ADD_QUESTIONCARD");
	}
	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.questioncardForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}


	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.questioncardForm.controls;

		if (this.questioncardForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			let formDataToPost = this.generateSubmitData(this.questioncardForm.value);
			this.questioncardService.createOrEdit(formDataToPost).subscribe(
				data => {   console.log('success', data); this.dialogRef.close({ questioncard: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}


	}

	generateSubmitData(formData) {
		var tenant = {
			id: Number(formData.tenantId)
		};
		formData.tenant = tenant;

		var topic = {
			id: Number(formData.topicId)
		};
		formData.topic = topic;

		var lesson = {
			id: Number(formData.lessonId)
		};
		formData.lesson = lesson;
		return formData;
	}

	updateCustomer(_questioncard: any) {

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
	createCustomer(_questioncard: any) {
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

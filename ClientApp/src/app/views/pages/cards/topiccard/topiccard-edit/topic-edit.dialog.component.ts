// Angular
import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// Material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TopicCardService } from '../../../../../core/_base/layout/services/topiccard.service';
import { TranslateService } from '@ngx-translate/core';
import { TenantService } from '../../../../../core/_base/layout/services/tenant.service';
import { LessonService } from '../../../../../../app/core/_base/layout/services/lesson.service';
import { TopicService } from '../../../../../../app/core/_base/layout/services/topic.service';

@Component({
	selector: 'kt-topiccards-edit-dialog',
	templateUrl: './topiccard-edit.dialog.component.html'
})
export class TopicCardEditDialogComponent implements OnInit, OnDestroy {
	topiccard = {
		id: 0,
		name: "",
		createdData: Date,
		description: "",
		tenantId: 0,
		isActive: true,
		fileUrl: "",
		price: undefined,
		actions: "",
		topicId: 0,
		lessonId: 0
	};

	topiccardForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	allTenants: any;
	allLessons: any;
	allTopics: any;

	topicsToShow = new Array<any>();

	constructor(public dialogRef: MatDialogRef<TopicCardEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private topiccardService: TopicCardService,
		public translate: TranslateService,
		private tenantService: TenantService,
		private lessonService: LessonService,
		private topicService: TopicService
	) {
	}


	ngOnInit() {
		if (this.data.topiccardModel) {
			this.topiccard = this.data.topiccardModel;
			this.topiccard.tenantId = this.data.topiccardModel.tenant.id;
			this.topiccard.lessonId = this.data.topiccardModel.lesson.id;
			this.topiccard.topicId = this.data.topiccardModel.topic.id;
		}

		this.createForm();
		this.getAllResources();
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

		this.topiccardForm = this.fb.group({
			id: [this.topiccard.id],
			name: [this.topiccard.name, Validators.required],
			description: [this.topiccard.description],
			tenantId: [this.topiccard.tenantId, Validators.required],
			lessonId: [this.topiccard.lessonId, Validators.required],
			fileUrl: [this.topiccard.fileUrl, Validators.required],
			price: [this.topiccard.price, Validators.required],
			topicId: [this.topiccard.topicId, Validators.required]
			//isActive: [this.topiccard.isActive, Validators.required],
		});
	}

	fileUploadFinished(input) {
		this.topiccardForm.value.fileUrl = "https://localhost:5486/Upload/" + input.data.name;
		this.topiccardForm.controls["fileUrl"].setValue(this.topiccardForm.value.fileUrl);
	}

	lessonChangeListener($event) {
		this.calculateTopicsForLesson();
	}

	calculateTopicsForLesson() {
		var lessonId = this.topiccardForm.value.lessonId;
		this.topicsToShow = new Array<any>();
		if (this.allTopics) {
			for (let topic of this.allTopics) {
				if (lessonId == topic.lessonId) {
					this.topicsToShow.push(topic);
				}
			}
		}
	}

	getTitle(): string {
		if (this.topiccard.id > 0) {
			return this.translate.instant("TOPICCARD.EDIT_TOPICCARD");
		}
		return this.translate.instant("TOPICCARD.ADD_TOPICCARD");
	}
	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.topiccardForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.topiccardForm.controls;

		if (this.topiccardForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			let formDataToPost = this.generateSubmitData(this.topiccardForm.value);
			this.topiccardService.createOrEdit(formDataToPost).subscribe(
				data => {   console.log('success', data); this.dialogRef.close({ topiccard: data, isEdit: false }); },
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

	/** Alect Close event */
	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

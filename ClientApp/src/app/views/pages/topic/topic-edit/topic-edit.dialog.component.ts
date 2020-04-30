import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TopicService } from '../../../../core/_base/layout';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'kt-topic-edit.dialog',
	templateUrl: './topic-edit.dialog.component.html',
	styleUrls: ['./topic-edit.dialog.component.scss']
})
export class TopicEditDialogComponent implements OnInit {

	topic = { id: 0, name: '',lessonId:0, classLevel: '', parentTopicId: 0 };
	lessons;
	topics;
	lessonTopics;

	topicForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	constructor(
		public dialogRef: MatDialogRef<TopicEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private topicService: TopicService,
		public translate: TranslateService
	) { }

	ngOnInit() {
		 
		if (this.data.topicModel) {
			this.topic = this.data.topicModel;
		}
		this.topicService.fetchAllLessons().subscribe((data: any) => {
			this.lessons = data;
		});
		this.topicService.fetchAllActiveTopics().subscribe((data: any) => {
			this.topics = data;
			this.calculateTopicForLesson();
		});
		this.createForm();
	}

	createForm() {
		this.topicForm = this.fb.group({
			id: [this.topic.id],
			name: [this.topic.name, Validators.required],
			lessonId: [this.topic.lessonId, Validators.compose([Validators.required,Validators.min(1)])],
			classLevel: [this.topic.classLevel, Validators.compose([Validators.required,Validators.min(1),Validators.max(12)])],
			parentTopicId:[this.topic.parentTopicId]
		});
	}

	calculateTopicForLesson() {
		var lessonId = this.topicForm.value.lessonId;
		this.lessonTopics = new Array<any>();
		if (this.topics) {
			for (let topic of this.topics) {
				if (topic.lessonId == lessonId) {
					this.lessonTopics.push(topic);
				}
			}
		}
	}

	lessonChangeListener($event) {
		this.calculateTopicForLesson();
	}

	getTitle(): string {
		if (this.topic.id > 0) {
			return this.translate.instant("TOPIC.EDIT_TOPIC");
		}
		return this.translate.instant("TOPIC.NEW_TOPIC");
	}

	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.topicForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.topicForm.controls;
		if (this.topicForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			debugger;
			this.topicForm.value.lessonId = Number(this.topicForm.value.lessonId);
			this.topicForm.value.parentTopicId = Number(this.topicForm.value.parentTopicId);
			console.log(this.topicForm.value);
			this.topicService.createOrEditTopic(this.topicForm.value).subscribe(
				data => { console.log('success', data); this.dialogRef.close({ topic: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

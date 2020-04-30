// Angular
import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
// Material
import { MatDialogRef, MAT_DIALOG_DATA, MatSelect } from '@angular/material';
import { TenantService } from '../../../../core/_base/layout';
import { TranslateService } from '@ngx-translate/core';
import { BundleService } from '../../../../services/bundle.service';
import { VERSION } from '@angular/material';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil, take } from 'rxjs/operators';
import { ExamCardService } from '../../../../core/_base/layout/services/examcard.service';
import { QuestionCardService } from '../../../../core/_base/layout/services/questioncard.service';
import { TopicCardService } from '../../../../core/_base/layout/services/topiccard.service';

interface Bank {
	id: string;
	name: string;
}


@Component({
	// tslint:disable-next-line:component-selector
	selector: 'bundle-edit',
	templateUrl: './bundle.dialog.component.html',
})
export class BundleDialogComponent implements OnInit, OnDestroy {


	version = VERSION;


	/** control for the selected bank for multi-selection */
	public questionCardMultiCtrl: FormControl = new FormControl();
	/** control for the MatSelect filter keyword multi-selection */
	public questionCardMultiFilterCtrl: FormControl = new FormControl();
	/** list of banks filtered by search keyword for multi-selection */
	public filteredQuestionCardMulti: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);



	/** control for the selected bank for multi-selection */
	public topicCardMultiCtrl: FormControl = new FormControl();
	/** control for the MatSelect filter keyword multi-selection */
	public topicCardMultiFilterCtrl: FormControl = new FormControl();
	/** list of banks filtered by search keyword for multi-selection */
	public filteredTopicCardMulti: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);


	/** control for the selected bank for multi-selection */
	public examCardMultiCtrl: FormControl = new FormControl();
	/** conexamfor the MatSelect filter keyword multi-selection */
	public examCardMultiFilterCtrl: FormControl = new FormControl();
	/** lisexambanks filtered by search keyword for multi-selection */
	public filteredExamCardMulti: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);


	/** Subject that emits when the component has been destroyed. */
	private _onDestroy = new Subject<void>();

	//dadawdawdaw
	bundle = {
		name: '',
		description: '',
		price: 0,
		tenantId: '',
		id: 0,
		questionCards: [],
		examCards: [],
		topicCards: []
	};

	subscribeFilter() {
		this.questionCardMultiFilterCtrl.valueChanges
			.pipe(takeUntil(this._onDestroy))
			.subscribe(() => {
				this.filterQuestionCardMulti();
			});

		this.topicCardMultiFilterCtrl.valueChanges
			.pipe(takeUntil(this._onDestroy))
			.subscribe(() => {
				this.filterTopicCardMulti();
			});

		this.examCardMultiFilterCtrl.valueChanges
			.pipe(takeUntil(this._onDestroy))
			.subscribe(() => {
				this.filterExamCardMulti();
			});
	}


	private filterQuestionCardMulti() {
		if (!this.questionCardList) {
			return;
		}
		// get the search keyword
		let search = this.questionCardMultiFilterCtrl.value;
		if (!search) {
			this.filteredQuestionCardMulti.next(this.questionCardList.slice());
			return;
		} else {
			search = search.toLowerCase();
		}
		// filter the banks
		this.filteredQuestionCardMulti.next(
			this.questionCardList.filter(bank => bank.name.toLowerCase().indexOf(search) > -1)
		);
	}

	private filterTopicCardMulti() {
		if (!this.topicCardList) {
			return;
		}
		// get the search keyword
		let search = this.topicCardMultiFilterCtrl.value;
		if (!search) {
			this.filteredTopicCardMulti.next(this.topicCardList.slice());
			return;
		} else {
			search = search.toLowerCase();
		}
		// filter the banks
		this.filteredTopicCardMulti.next(
			this.topicCardList.filter(bank => bank.name.toLowerCase().indexOf(search) > -1)
		);
	}

	private filterExamCardMulti() {
		if (!this.examCardList) {
			return;
		}
		// get the search keyword
		let search = this.examCardMultiFilterCtrl.value;
		if (!search) {
			this.filteredExamCardMulti.next(this.questionCardList.slice());
			return;
		} else {
			search = search.toLowerCase();
		}
		// filter the banks
		this.filteredExamCardMulti.next(
			this.examCardList.filter(bank => bank.name.toLowerCase().indexOf(search) > -1)
		);
	}

	bundleForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;
	questionCards: any;
	constructor(public dialogRef: MatDialogRef<BundleDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		public translate: TranslateService,
		private bundleService: BundleService,
		private examCardService: ExamCardService,
		private questionCardService: QuestionCardService,
		private topicCardService: TopicCardService,
		private tenantService: TenantService
	) {

	}
	questionCardList;
	topicCardList;
	examCardList;
	tenants;


	fetchSelectDatas() {
		this.questionCardService.fetchAll().subscribe(
			data => {
				this.questionCardList = data;
				this.filteredQuestionCardMulti.next(this.questionCardList.slice());
				console.log("questionList:" + JSON.stringify(data));
			},
			error => console.log('oops', error)
		);

		this.topicCardService.fetchAll().subscribe(
			data => {
				this.topicCardList = data;
				this.filteredTopicCardMulti.next(this.topicCardList.slice());
			},
			error => console.log('oops', error)
		);

		this.examCardService.fetchAll().subscribe(
			data => {
				this.examCardList = data;
				this.filteredExamCardMulti.next(this.examCardList.slice());
			},
			error => console.log('oops', error)
		);

		this.tenantService.fetchAllTenants().subscribe((data: any) => {
			this.tenants = data;
		});

	}


	ngOnInit() {
		 
		this.fetchSelectDatas();
		this.subscribeFilter();
		if (this.data.bundleModel) {

			var selectedQuestionCardIdList = this.data.bundleModel.questionCards.map(function (item) {
				return item.id
			});
			this.questionCardMultiCtrl.setValue(selectedQuestionCardIdList);

			var selectedExamCardIdList = this.data.bundleModel.examCards.map(function (item) {
				return item.id
			});
			this.examCardMultiCtrl.setValue(selectedExamCardIdList);

			var selectedTopicCardIdList = this.data.bundleModel.topicCards.map(function (item) {
				return item.id
			});
			this.topicCardMultiCtrl.setValue(selectedTopicCardIdList);

			this.bundle = this.data.bundleModel;
			this.bundle.tenantId = this.data.bundleModel.tenant.id;
		}
		this.createForm();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}

	createForm() {
		this.bundleForm = this.fb.group({
			id: [this.bundle.id],
			name: [this.bundle.name, Validators.required],
			tenantId: [this.bundle.tenantId, Validators.required],
			questionCards: [this.bundle.questionCards],
			examCards: [this.bundle.examCards],
			topicCards: [this.bundle.topicCards],
			price: [this.bundle.price, Validators.compose([Validators.nullValidator])],
			description: [this.bundle.description, Validators.compose([Validators.minLength(5), Validators.required])]
		});

	}

	getTitle(): string {
		if (this.bundle.id > 0) {
			return this.translate.instant("EDIT_BUNDLE");
		}
		return this.translate.instant("ADD_BUNDLE");
	}

	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.bundleForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {

		this.hasFormErrors = false;
		const controls = this.bundleForm.controls;

		if (this.bundleForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;

		} else {

			this.bundleForm.value.typeId = Number(this.bundleForm.value.typeId);

			let questionCardObjectList = this.questionCardMultiCtrl.value ?
				this.questionCardMultiCtrl.value.map(function (id) {
					return { "id": id }
				}) : [];

			let topicCardObjectList = this.topicCardMultiCtrl.value ?
				this.topicCardMultiCtrl.value.map(function (id) {
					return { "id": id }
				}) : [];

			let examCardObjectList = this.examCardMultiCtrl.value ?
				this.examCardMultiCtrl.value.map(function (id) {
					return { "id": id }
				}) : [];

			var postData = {
				"id": this.bundleForm.value.id,
				"name": this.bundleForm.value.name,
				"description": this.bundleForm.value.description,
				"price": this.bundleForm.value.price,
				"questionCards": questionCardObjectList,
				"examCards": examCardObjectList,
				"topicCards": topicCardObjectList,
				"tenant": { "id": this.bundleForm.value.tenantId },
				"fileUrl": ""//TODO gönderilmezse patlıyor. nullable olmalı
			};

			this.bundleService.createOrEditBundle(postData).subscribe(
				data => { this.dialogRef.close({ tenant: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}

}

// Angular
import { Component, OnInit, Inject, ChangeDetectionStrategy, ViewEncapsulation, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// Material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ExamCardService } from '../../../../../core/_base/layout/services/examcard.service';
import { TranslateService } from '@ngx-translate/core';
import { TenantService } from '../../../../../core/_base/layout/services/tenant.service';

@Component({
	selector: 'kt-examcards-edit-dialog',
	templateUrl: './examcard-edit.dialog.component.html'
})
export class ExamCardEditDialogComponent implements OnInit, OnDestroy {
	examcard = {
		id: 0,
		name: "",
		createdData: Date,
		examTypeId: undefined,
		examTypeIdString : "",
		description: "",
		tenantId: 0,
		isActive: true,
		questionCount: undefined,
		fileUrl: "",
		price: undefined,
		actions: ""
	};

	examTypeIdToString = ["YKS","TYT","AYT","LGS","KPSS","AÖÖ","AÖL","MAÖL","AÖİHL"];

	examcardForm: FormGroup;
	hasFormErrors = false;
	viewLoading = false;

	allTenants: any;
	
	constructor(public dialogRef: MatDialogRef<ExamCardEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private examcardService: ExamCardService,
		public translate: TranslateService,
		private tenantService: TenantService		
	) {
	}


	ngOnInit() {
		console.log("Createexamcard:" + this.data.examcardModel);
		if (this.data.examcardModel){
			this.examcard = this.data.examcardModel;
			this.examcard.tenantId = this.data.examcardModel.tenant.id;
			this.examcard.examTypeIdString = this.data.examcardModel.examTypeId + "";
		}
			
		this.createForm();
		this.getAllResources();
	}

	getAllResources(){
		this.tenantService.fetchAllTenants().subscribe((data: any) => {
			this.allTenants = data;
		});
	}

	ngOnDestroy() {

	}
	createForm() {

		this.examcardForm = this.fb.group({
			id: [this.examcard.id],
			name: [this.examcard.name, Validators.required],
			description: [this.examcard.description],
			tenantId: [this.examcard.tenantId, Validators.required],
			examTypeIdString: [this.examcard.examTypeIdString, Validators.required],
			questionCount: [this.examcard.questionCount, Validators.required],
			fileUrl: [this.examcard.fileUrl, Validators.required],
			price: [this.examcard.price, Validators.required]
		});
	}

	fileUploadFinished(input){
		this.examcardForm.value.fileUrl = "https://localhost:5486/Upload/" + input.data.name;
		this.examcardForm.controls["fileUrl"].setValue(this.examcardForm.value.fileUrl);
	}

	getTitle(): string {
		if (this.examcard.id > 0) {
			return this.translate.instant("EXAMCARD.EDIT_EXAMCARD");
		}
		return this.translate.instant("EXAMCARD.ADD_EXAMCARD");
	}
	dialogClose(): void {
		this.dialogRef.close();
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.examcardForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	onSubmit() {
		this.hasFormErrors = false;
		const controls = this.examcardForm.controls;
		
		if (this.examcardForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			this.hasFormErrors = true;
			return;
		} else {
			let formDataToPost = this.generateSubmitData(this.examcardForm.value);
			this.examcardService.createOrEdit(formDataToPost).subscribe(
				data => { console.log('success', data); this.dialogRef.close({ examcard: data, isEdit: false }); },
				error => console.log('oops', error)
			);
		}
		

	}

	generateSubmitData(formData){
		var tenant = {
			id : Number(formData.tenantId)
		};
		formData.tenant = tenant;
		formData.examTypeId = Number(formData.examTypeIdString);
		delete formData.examTypeIdString;
		
		return formData;
	}

	/** Alect Close event */
	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}

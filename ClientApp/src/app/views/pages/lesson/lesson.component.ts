import { Component, OnInit, ElementRef, ViewChild, ChangeDetectionStrategy, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { SelectionModel, DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog, MatTableDataSource, Sort } from '@angular/material';
import { debounceTime, distinctUntilChanged, tap, skip, delay, take } from 'rxjs/operators';
import { fromEvent, merge, Subscription, of } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { LessonEditDialogComponent } from './lesson-edit/lesson-edit.dialog.component';
import { LessonService } from '../../../core/_base/layout';
import { Router } from '@angular/router';

@Component({
	selector: 'kt-lessons-list',
	templateUrl: './lesson.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LessonComponent implements OnInit, OnDestroy {

	locale = 'tr';
	initialData = new Array<LessonData>();
	dataSource = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'name',
		'educationLevel',
		'isActive',
		'createdDate',
		'id'
	];

	educationLevelToString = ["İlkokul","Lise","Üniversite"];
	

	lessonsResult = [];

	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;

	selection = new SelectionModel<any>(true, []);
	sortedData;
	private subscriptions: Subscription[] = [];
	constructor(
		public dialog: MatDialog,
		public snackBar: MatSnackBar,
		private layoutUtilsService: LayoutUtilsService,
		public translate: TranslateService,
		private lessonService: LessonService,
		
	) {

	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = (filterValue.trim() as any).toLocaleLowerCase(this.locale);
	}

	registerFilterPredicate() {
		this.dataSource.filterPredicate = (data, filter) => {
			const dataStr = this.getFilterString(data);
			return dataStr.indexOf(filter) != -1;
		}
	}

	ngOnInit() {
		this.loadDataTable();
		this.registerFilterPredicate();
	}

	ngAfterViewInit() {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	loadDataTable() {
		this.lessonService.fetchAllLessons().subscribe((data: any) => {
			debugger;
			this.dataSource.data = data;
		});

	}

	openCreateEditDialog(isAdd: Boolean, lesson: any) {
		let lessonModel;

		if (!isAdd)
			lessonModel = lesson;
		let saveMessageTranslateParam = 'LESSON.EDIT.';
		saveMessageTranslateParam += !isAdd ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(LessonEditDialogComponent, { data: { lessonModel } })

		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			 
			console.log('datasource: ' + this.dataSource + 'res: ' + res);
			let data = this.dataSource.data;

			if (isAdd) {
				data.push(res.lesson);
				this.dataSource.data = data;
			}
			else {
				this.dataSource.data = data.map(a => {
					 
					if (a["id"] == res.lesson.id)
						return res.lesson;
					return a;
				});

			}

		});
	}

	deleteLesson(id) {
		 var _title: string = this.translate.instant('LESSON.LESSON');
		
		const dialogRef = this.layoutUtilsService.deleteElement( _title + " silme", _title + " silinecek emin misiniz?", "Siliniyor...");
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.dataSource.data;

			this.lessonService.DeleteLessonById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data.id;
					});
					dataSrc.splice(index, 1);
					this.dataSource.data = dataSrc;
				},
				error => console.log('oops', error)
			);
			var deleteTitle: string = this.translate.instant('DELETE');
			this.layoutUtilsService.showActionNotification(deleteTitle, MessageType.Delete);
		});
	}


	isAllSelected(): boolean {
		const numSelected = this.selection.selected.length;
		const numRows = this.lessonsResult.length;
		return numSelected === numRows;
	}

	masterToggle() {
		if (this.selection.selected.length === this.lessonsResult.length) {
			this.selection.clear();
		} else {
			this.lessonsResult.forEach(row => this.selection.select(row));
		}
	}

	ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	StringsOrEmpty = function( entity ) {
		return entity || "";
	};

	public getFilterString(data: LessonData): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(this.educationLevelToString[data.educationLevel]));
		result = result.concat(this.StringsOrEmpty(data.isActive));
		result = result.concat(this.StringsOrEmpty(data.createdDate));
		result = result.concat(this.StringsOrEmpty(data.id));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}

}

export class LessonData {
	select: any;
	name: string;
	educationLevel: number;
	topics:any;
	isActive: boolean;
	createdDate: Date;
	id: number;
	actions: any;
}

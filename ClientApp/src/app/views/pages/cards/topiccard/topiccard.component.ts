import { Component, OnInit, ElementRef, ViewChild, ChangeDetectionStrategy, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { SelectionModel, DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog, MatTableDataSource, Sort } from '@angular/material';
import { debounceTime, distinctUntilChanged, tap, skip, delay, take } from 'rxjs/operators';
import { fromEvent, merge, Subscription, of } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { LayoutUtilsService, MessageType } from '../../../../core/_base/crud/utils/layout-utils.service';
import { TopicCardEditDialogComponent } from './topiccard-edit/topic-edit.dialog.component';
import { TopicCardService } from '../../../../core/_base/layout/services/topiccard.service';
import { TenantData } from '../../tenant/tenant.component';
import { TopicDataModel } from '../../topic/topic.component';
import { LessonData } from '../../lesson/lesson.component';

@Component({
	selector: 'kt-topiccards-list',
	templateUrl: './topiccard.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TopicCardComponent implements OnInit, OnDestroy {
	
	locale = 'tr';
	initialData = new Array<TopicCardData>();
	dataSource = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'name',
		'description',
		'tenant',
		'topic',
		'lesson',
		'fileUrl',
		'price',
		'isActive',
		'createdDate',
		'id'	
	];

	topiccardsResult = [];

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
		private topiccardService: TopicCardService
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
		this.topiccardService.fetchAll().subscribe((data: any) => {
			this.dataSource.data = data;
		});

	}

	openCreateEditDialog(isAdd: Boolean, topiccard: any) {
		let topiccardModel;

		if (!isAdd)
			topiccardModel = topiccard;
		let saveMessageTranslateParam = 'TOPICCARD.EDIT.';
		saveMessageTranslateParam += !isAdd ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(TopicCardEditDialogComponent, { data: { topiccardModel } })

		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			let data = this.dataSource.data;

			/*
			if (isAdd) {
				data.push(res.topiccard);
				this.dataSource.data = data;
			}
			else {
				this.dataSource.data = data.map(a => {
					if (a["id"] == res.topiccard.id)
						return res.topiccard;
					return a;
				});

			}
			*/
		this.loadDataTable();

		});
	}

	deleteTopicCard(id) {
		const dialogRef = this.layoutUtilsService.deleteElement(this.translate.instant('TOPICCARD.DELETE.INFO_MESSAGE'), this.translate.instant('TOPICCARD.DELETE.CONFIRM_MESSAGE'), this.translate.instant('TOPICCARD.DELETE.WAITING_MESSAGE'));
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.dataSource.data;

			this.topiccardService.deleteById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data.id;
					});
					dataSrc.splice(index, 1);
					this.dataSource.data = dataSrc;
				},
				error => console.log('oops', error)
			);
			this.layoutUtilsService.showActionNotification("Delete", MessageType.Delete);
		});
	}


	isAllSelected(): boolean {
		const numSelected = this.selection.selected.length;
		const numRows = this.topiccardsResult.length;
		return numSelected === numRows;
	}

	masterToggle() {
		if (this.selection.selected.length === this.topiccardsResult.length) {
			this.selection.clear();
		} else {
			this.topiccardsResult.forEach(row => this.selection.select(row));
		}
	}

	ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	StringsOrEmpty = function( entity ) {
		return entity || "";
	};

	public getFilterString(data: TopicCardData): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(data.description));
		result = result.concat(this.StringsOrEmpty(data.tenant.name));
		result = result.concat(this.StringsOrEmpty(data.topic.name));
		result = result.concat(this.StringsOrEmpty(data.lesson.name));
		result = result.concat(this.StringsOrEmpty(data.fileUrl));
		result = result.concat(this.StringsOrEmpty(data.isActive));
		result = result.concat(this.StringsOrEmpty(data.createdDate));
		result = result.concat(this.StringsOrEmpty(data.id));
		result = result.concat(this.StringsOrEmpty(data.price));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}

}

export class TopicCardData {
	select: any;
	name: string;
	description: string;
	tenant: TenantData;
	fileUrl: string;
	price: number;
	isActive: boolean;
	createdDate: Date;
	id: number;
	actions: any;
	topic: TopicDataModel;
	lesson: LessonData;
}

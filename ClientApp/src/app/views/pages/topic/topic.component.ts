import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar, MatTableDataSource } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { merge, Subscription } from 'rxjs';
import { MessageType, LayoutUtilsService } from '../../../core/_base/crud';
import { TopicEditDialogComponent } from './topic-edit/topic-edit.dialog.component';
import { SelectionModel } from '@angular/cdk/collections';
import { TopicService } from '../../../core/_base/layout/services/topic.service';
import { LessonData } from '../lesson/lesson.component';

@Component({
	selector: 'kt-topic',
	templateUrl: './topic.component.html',
	styleUrls: ['./topic.component.scss'],
})
export class TopicComponent implements OnInit, OnDestroy {

	locale = 'tr';
	initialData = new Array<TopicDataModel>();
	topicList = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'name',
		'lesson',
		'class',
		'parent',
		'actions'
	];

	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
	@ViewChild('sort1', { static: true }) sort: MatSort;
	selection = new SelectionModel<any>(true, []);
	sortedData;

	private subscriptions: Subscription[] = [];
	constructor(
		public dialog: MatDialog,
		public snackBar: MatSnackBar,
		public translate: TranslateService,
		private layoutUtilsService: LayoutUtilsService,
		private topicService: TopicService
	) { }

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.topicList.filter = (filterValue.trim() as any).toLocaleLowerCase(this.locale);
	}

	registerFilterPredicate() {
		this.topicList.filterPredicate = (data, filter) => {
			const dataStr = this.getFilterString(data);
			return dataStr.indexOf(filter) != -1;
		}
	}

	ngOnInit() {
		this.loadDataTable();
		this.registerFilterPredicate();
	}

	ngAfterViewInit() {
		this.topicList.paginator = this.paginator;
		this.topicList.sort = this.sort;
	}

	loadDataTable() {
		this.topicService.fetchAllActiveTopics().subscribe((data: any) => {
			console.log(data);
			this.topicList.data = data;
		});
	}

	openCreateEditDialog(isAdd: Boolean, topic: any) {
		 
		let topicModel;
		if (!isAdd) {
			topicModel = topic;
		}
		let saveMessageTranslateParam = 'TOPIC.';
		saveMessageTranslateParam += !isAdd ? 'EDIT_MESSAGE' : 'SAVE_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(TopicEditDialogComponent, { data: { topicModel } })
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			console.log('datasource: ' + this.topicList + 'res: ' + res);
			let data = this.topicList.data;

			if (isAdd) {
				data.push(res.topic);
				this.topicList.data = data;
			} else {
				this.topicList.data = data.map(a => {
					if (a["id"] == res.topic.id)
						return res.topic;
					return a;
				});
			}

		});
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	deleteTopic(id) {
		const dialogRef = this.layoutUtilsService.deleteElement(this.translate.instant('TOPIC.DELETE_DIALOG.INFO_MESSAGE'),
			this.translate.instant('TOPIC.DELETE_DIALOG.CONFIRM_MESSAGE'), this.translate.instant('TOPIC.DELETE_DIALOG.WAITING_MESSAGE'));
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.topicList.data;
			this.topicService.deleteTopicById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data["id"];
					});
					dataSrc.splice(index, 1);
					this.topicList.data = dataSrc;
				},
				error => console.log("oops", error)
			);
			this.layoutUtilsService.showActionNotification(this.translate.instant('TOPIC.DELETE_MESSAGE'), MessageType.Delete);
		});
	}

	StringsOrEmpty = function (entity) {
		return entity || "";
	};

	public getFilterString(data: TopicDataModel): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(data.lessonName));
		result = result.concat(this.StringsOrEmpty(data.class));
		result = result.concat(this.StringsOrEmpty(data.parentTopicName));
		result = result.concat(this.StringsOrEmpty(data.isActive));
		result = result.concat(this.StringsOrEmpty(data.createdDate));
		result = result.concat(this.StringsOrEmpty(data.id));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}
}

export class TopicDataModel {
	name: string;
	lesson: LessonData;
	lessonName: string;
	class: Number;
	parentTopicName: string;
	parent: Number;
	isActive: boolean;
	createdDate: Date;
	id: number;
}

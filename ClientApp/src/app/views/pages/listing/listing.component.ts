import { Component, OnInit, OnDestroy, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar, MatTableDataSource } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { merge, Subscription } from 'rxjs';
import { MessageType, LayoutUtilsService } from '../../../core/_base/crud';
import { ListingEditDialogComponent } from './listing-edit/listing-edit.dialog.component';
import { SelectionModel } from '@angular/cdk/collections';
import { ProductCategoryService } from '../../../core/_base/layout/services/productcategory.service';
import { LessonData } from '../lesson/lesson.component';
import { ListingService } from '../../../core/_base/layout/services/listing.service';

@Component({
	selector: 'kt-listing',
	templateUrl: './listing.component.html',
	styleUrls: ['./listing.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListingComponent implements OnInit, OnDestroy {

	locale = 'tr';
	initialData = new Array<ListingDataModel>();
	ListingList = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'name',
		'description',
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
		private listingService: ListingService
	) { }

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.ListingList.filter = (filterValue.trim() as any).toLocaleLowerCase(this.locale);
	}

	registerFilterPredicate() {
		this.ListingList.filterPredicate = (data, filter) => {
			const dataStr = this.getFilterString(data);
			return dataStr.indexOf(filter) != -1;
		}
	}

	ngOnInit() {
		this.loadDataTable();
		this.registerFilterPredicate();
	}

	ngAfterViewInit() {
		this.ListingList.paginator = this.paginator;
		this.ListingList.sort = this.sort;
	}

	loadDataTable() {
		this.listingService.fetchAllListings().subscribe((data: any) => {
			debugger;
			console.log(data);
			this.ListingList.data = data;
		});
	}

	openCreateEditDialog(isAdd: Boolean, listing: any) {
		debugger;
		let listingModel;
		if (!isAdd) {
			listingModel = listing;
		}
		let saveMessageTranslateParam = 'LISTING.';
		saveMessageTranslateParam += !isAdd ? 'EDIT_MESSAGE' : 'SAVE_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(ListingEditDialogComponent, { data: { listingModel } })
		dialogRef.afterClosed().subscribe(res => {
			debugger;
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			console.log('datasource: ' + this.ListingList + 'res: ' + res);
			let data = this.ListingList.data;

			if (isAdd) {
				data.push(res.listing);
				this.ListingList.data = data;
			} else {
				this.ListingList.data = data.map(a => {
					if (a["id"] == res.listing.id)
						return res.listing;
					return a;
				});
			}

		});
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	deleteListing(id) {
		debugger;
		const dialogRef = this.layoutUtilsService.deleteElement(this.translate.instant('LISTING.DELETE_DIALOG.INFO_MESSAGE'),
			this.translate.instant('LISTING.DELETE_DIALOG.CONFIRM_MESSAGE'), this.translate.instant('LISTING.DELETE_DIALOG.WAITING_MESSAGE'));
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.ListingList.data;
			this.listingService.DeleteListingById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data["id"];
					});
					dataSrc.splice(index, 1);
					this.ListingList.data = dataSrc;
				},
				error => console.log("oops", error)
			);
			this.layoutUtilsService.showActionNotification(this.translate.instant('LISTING.DELETE_MESSAGE'), MessageType.Delete);
		});
	}

	StringsOrEmpty = function (entity) {
		return entity || "";
	};

	public getFilterString(data: ListingDataModel): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(data.description));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}
}

export class ListingDataModel {
	id: number;
	name: string;
	description: string;
	isActive: boolean;
	createdDate: Date;
	
}


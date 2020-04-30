import { Component, OnInit, ElementRef, ViewChild, ChangeDetectionStrategy, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { SelectionModel, DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog, MatTableDataSource, Sort } from '@angular/material';
import { Subscription } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { BundleService } from '../../../services/bundle.service';
import { BundleDialogComponent } from './bundle-edit/bundle.dialog.component';


@Component({
	selector: 'bundle',
	templateUrl: './bundle.component.html'
})
export class BundleComponent implements OnInit, OnDestroy {

	dataSource = new MatTableDataSource();

	displayedColumns = [
		'name',
		'description',
		'price',
		'tenant',
		'id',
	];

	bundleResult = [];

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
		public bundleService: BundleService
	) {

	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();
	}

	ngOnInit() {
		 
		this.loadDataTable();
	}

	ngAfterViewInit() {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}
  
	loadDataTable() {
		 this.bundleService.fetchAllBundles().subscribe((data: any) => {
		 	this.dataSource.data = data;
		 });
	}

	openCreateEditDialog(isAdd: Boolean, model: any) {
		let bundleModel;

		if (!isAdd)
		bundleModel = model;
		let saveMessageTranslateParam = !isAdd ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(BundleDialogComponent, { data: { bundleModel } })

		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			 
			console.log('datasource: ' + this.dataSource + 'res: ' + res);
			let data = this.dataSource.data;

			// if (isAdd) {
			// 	data.push(res.questionCard);
			// 	this.dataSource.data = data;
			// }
			// else {
			// 	this.dataSource.data = data.map(a => {
			// 		 
			// 		if (a["id"] == res.tenant.id)
			// 			return res.tenant;
			// 		return a;
			// 	});

			// }

			this.loadDataTable();

		});
	}

	deleteBundle(id) {
		// const _title: string = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.TITLE');
		// const _description: string = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.DESCRIPTION');
		// const _waitDesciption: string = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.WAIT_DESCRIPTION');
		// const _deleteMessage = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.MESSAGE');
		const dialogRef = this.layoutUtilsService.deleteElement("Bundle kartı silme", "Bundle kartı silinecek emin misiniz?", "Siliniyor...");
		 dialogRef.afterClosed().subscribe(res => {
		 	if (!res) {
		 		return;
		 	}
		 	let dataSrc = this.dataSource.data
		 	this.bundleService.deleteBundleById(id).subscribe(
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


	//isAllSelected(): boolean {
	//	const numSelected = this.selection.selected.length;
	//	const numRows = this.bundleResult.length;
	//	return numSelected === numRows;
	//}

	masterToggle() {
		if (this.selection.selected.length === this.bundleResult.length) {
			this.selection.clear();
		} else {
			this.bundleResult.forEach(row => this.selection.select(row));
		}
	}

	ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

}


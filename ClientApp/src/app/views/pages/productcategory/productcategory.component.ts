import { Component, OnInit, OnDestroy, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { MatPaginator, MatSort, MatDialog, MatSnackBar, MatTableDataSource } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { merge, Subscription } from 'rxjs';
import { MessageType, LayoutUtilsService } from '../../../core/_base/crud';
import { ProductCategoryEditDialogComponent } from './productcategory-edit/productcategory-edit.dialog.component';
import { SelectionModel } from '@angular/cdk/collections';
import { ProductCategoryService } from '../../../core/_base/layout/services/productcategory.service';
import { LessonData } from '../lesson/lesson.component';

@Component({
	selector: 'kt-productcategory',
	templateUrl: './productcategory.component.html',
	styleUrls: ['./productcategory.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProductCategoryComponent implements OnInit, OnDestroy {

	locale = 'tr';
	initialData = new Array<ProductCategoryDataModel>();
	ProductCategoryList = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'categoryName',
		'listingName',
		'parentCategoryName',
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
		private productCategoryService: ProductCategoryService
	) { }

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.ProductCategoryList.filter = (filterValue.trim() as any).toLocaleLowerCase(this.locale);
	}

	registerFilterPredicate() {
		this.ProductCategoryList.filterPredicate = (data, filter) => {
			const dataStr = this.getFilterString(data);
			return dataStr.indexOf(filter) != -1;
		}
	}

	ngOnInit() {
		this.loadDataTable();
		this.registerFilterPredicate();
	}

	ngAfterViewInit() {
		this.ProductCategoryList.paginator = this.paginator;
		this.ProductCategoryList.sort = this.sort;
	}

	loadDataTable() {
		this.productCategoryService.fetchAllActiveProductCategories().subscribe((data: any) => {
			debugger;
			console.log(data);
			this.ProductCategoryList.data = data;
		});
	}

	openCreateEditDialog(isAdd: Boolean, ProductCategory: any) {
		 
		let ProductCategoryModel;
		if (!isAdd) {
			ProductCategoryModel = ProductCategory;
		}
		let saveMessageTranslateParam = 'PRODUCTCATEGORY.';
		saveMessageTranslateParam += !isAdd ? 'EDIT_MESSAGE' : 'SAVE_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(ProductCategoryEditDialogComponent, { data: { ProductCategoryModel } })
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			console.log('datasource: ' + this.ProductCategoryList + 'res: ' + res);
			let data = this.ProductCategoryList.data;

			if (isAdd) {
				data.push(res.category);
				this.ProductCategoryList.data = data;
			} else {
				this.ProductCategoryList.data = data.map(a => {
					if (a["id"] == res.category.id)
						return res.category;
					return a;
				});
			}

		});
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	deleteProductCategory(id) {
		const dialogRef = this.layoutUtilsService.deleteElement(this.translate.instant('PRODUCTCATEGORY.DELETE_DIALOG.INFO_MESSAGE'),
			this.translate.instant('PRODUCTCATEGORY.DELETE_DIALOG.CONFIRM_MESSAGE'), this.translate.instant('PRODUCTCATEGORY.DELETE_DIALOG.WAITING_MESSAGE'));
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.ProductCategoryList.data;
			this.productCategoryService.deleteProductCategoryById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data["id"];
					});
					dataSrc.splice(index, 1);
					this.ProductCategoryList.data = dataSrc;
				},
				error => console.log("oops", error)
			);
			this.layoutUtilsService.showActionNotification(this.translate.instant('TOPIC.DELETE_MESSAGE'), MessageType.Delete);
		});
	}

	StringsOrEmpty = function (entity) {
		return entity || "";
	};

	public getFilterString(data: ProductCategoryDataModel): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.categoryName));
		result = result.concat(this.StringsOrEmpty(data.listingName));
		result = result.concat(this.StringsOrEmpty(data.parentCategoryName));
		result = result.concat(this.StringsOrEmpty(data.isActive));
		result = result.concat(this.StringsOrEmpty(data.createdDate));
		result = result.concat(this.StringsOrEmpty(data.id));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}
}

export class ProductCategoryDataModel {
	id: number;
	categoryName: string;
	parentCategoryName: string;
	parentCategoryId: number;
	listingId: number;
	listingName: string;
	isActive: boolean;
	createdDate: Date;
	
}
export class ListingData {
	name: string;
	isActive: boolean;
	createdDate: Date;
	id: number;
	actions: any;
}

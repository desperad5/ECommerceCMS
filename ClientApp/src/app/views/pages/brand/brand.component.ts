import { SelectionModel } from '@angular/cdk/collections';
import { ChangeDetectionStrategy, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatSnackBar, MatSort, MatTableDataSource } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { BrandService } from '../../../core/_base/layout/services/brand.service';
import { BrandModel } from '../../../models/brand-model';
import { BrandEditDialogComponent } from './brand-edit/brand-edit.dialog.component';
import { map } from 'rxjs/operators';
import { downloadFile, saveAs  } from 'file-saver';

@Component({
	selector: 'brandpage',
	templateUrl: './brand.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BrandPageComponent implements OnInit {
	locale = 'tr';
	initialData = new Array<BrandModel>();
	brandList = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'name',
		'webSiteUrl',
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
		private brandService: BrandService
	) { }

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.brandList.filter = (filterValue.trim() as any).toLocaleLowerCase(this.locale);
	}

	registerFilterPredicate() {
		this.brandList.filterPredicate = (data, filter) => {
			const dataStr = this.getFilterString(data);
			return dataStr.indexOf(filter) != -1;
		}
	}

	ngOnInit() {
		this.loadDataTable();
		this.registerFilterPredicate();
	}

	ngAfterViewInit() {
		this.brandList.paginator = this.paginator;
		this.brandList.sort = this.sort;
	}

	loadDataTable() {
		this.brandService.fetchAllActiveBrands().subscribe((data: any) => {
			console.log(data);
			this.brandList.data = data;
		});
	}

	openCreateEditDialog(isAdd: Boolean, brand: any) {

		let brandModel;
		if (!isAdd) {
			brandModel = brand;
		}
		let saveMessageTranslateParam = 'BRAND.';
		saveMessageTranslateParam += !isAdd ? 'EDIT_MESSAGE' : 'SAVE_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(BrandEditDialogComponent, { data: { brandModel } })
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			console.log('datasource: ' + this.brandList + 'res: ' + res);
			let data = this.brandList.data;

			if (isAdd) {
				data.push(res.brand);
				this.brandList.data = data;
			} else {
				this.brandList.data = data.map(a => {
					if (a["id"] == res.brand.id)
						return res.brand;
					return a;
				});
			}

		});
	}

	ngOnDestroy(): void {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	deleteTopic(id) {
		const dialogRef = this.layoutUtilsService.deleteElement(this.translate.instant('BRAND.DELETE_DIALOG.INFO_MESSAGE'),
			this.translate.instant('BRAND.DELETE_DIALOG.CONFIRM_MESSAGE'), this.translate.instant('BRAND.DELETE_DIALOG.WAITING_MESSAGE'));
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.brandList.data;
			this.brandService.deleteBrandById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data["id"];
					});
					dataSrc.splice(index, 1);
					this.brandList.data = dataSrc;
				},
				error => console.log("oops", error)
			);
			this.layoutUtilsService.showActionNotification(this.translate.instant('BRAND.DELETE_MESSAGE'), MessageType.Delete);
		});
	}

	StringsOrEmpty = function (entity) {
		return entity || "";
	};

	public getFilterString(data: BrandModel): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(data.id));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}

	public downloadExcel() {
		this.brandService.downloadExcel().subscribe(
			(blob) => {
				saveAs(blob, 'Markalar.xlsx');
		},
			(error) => { });
	}

}

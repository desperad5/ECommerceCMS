import { Component, OnInit, ElementRef, ViewChild, ChangeDetectionStrategy, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { SelectionModel, DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog, MatTableDataSource, Sort } from '@angular/material';
import { fromEvent, merge, Subscription, of } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { TenantEditDialogComponent } from './tenant-edit/tenant-edit.dialog.component';
import { TenantService } from '../../../core/_base/layout';

@Component({
	selector: 'kt-tenants-list',
	templateUrl: './tenant.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TenantComponent implements OnInit, OnDestroy {

	locale = 'tr';
	initialData = new Array<TenantData>();
	dataSource = new MatTableDataSource(this.initialData);

	displayedColumns = [
		'name',
		'typeId',
		'address',
		'phoneNumber',
		'taxNumber',
		'createdDate',
		'taxAdministration',
		'id'
	];

	//TODO: CONFIGden okunacak.
	findTenantTypeName(id) {
		if (id == 0)
			return this.translate.instant("PRIVATE_SECTOR");
		return this.translate.instant("PUBLIC_SECTOR")
	}

	tenantsResult = [];

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
		private tenantService: TenantService
	) {

	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();
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
		this.tenantService.fetchAllTenants().subscribe((data: any) => {
			 
			this.dataSource.data = data;
		});

	}

	openCreateEditDialog(isAdd: Boolean, tenant: any) {
		let tenantModel;

		if (!isAdd)
			tenantModel = tenant;
		let saveMessageTranslateParam = !isAdd ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(TenantEditDialogComponent, { data: { tenantModel } })

		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			 
			console.log('datasource: ' + this.dataSource + 'res: ' + res);
			let data = this.dataSource.data;

			if (isAdd) {
				data.push(res.tenant);
				this.dataSource.data = data;
			}
			else {
				this.dataSource.data = data.map(a => {
					 
					if (a["id"] == res.tenant.id)
						return res.tenant;
					return a;
				});

			}

		});
	}

	deleteTenant(id) {
		const dialogRef = this.layoutUtilsService.deleteElement("Tenant silme", "Tenant silinecek emin misiniz?", "Siliniyor...");
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.dataSource.data;

			this.tenantService.DeleteTenantById(id).subscribe(
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
		const numRows = this.tenantsResult.length;
		return numSelected === numRows;
	}

	masterToggle() {
		if (this.selection.selected.length === this.tenantsResult.length) {
			this.selection.clear();
		} else {
			this.tenantsResult.forEach(row => this.selection.select(row));
		}
	}

	ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	StringsOrEmpty = function (entity) {
		return entity || "";
	};

	public getFilterString(data: TenantData): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(data.typeId));
		result = result.concat(this.StringsOrEmpty(data.address));
		result = result.concat(this.StringsOrEmpty(data.county));
		result = result.concat(this.StringsOrEmpty(data.town));
		result = result.concat(this.StringsOrEmpty(data.phoneNumber));
		result = result.concat(this.StringsOrEmpty(data.logo));
		result = result.concat(this.StringsOrEmpty(data.taxNumber));
		result = result.concat(this.StringsOrEmpty(data.createdDate));
		result = result.concat(this.StringsOrEmpty(data.taxAdministration));
		result = result.concat(this.StringsOrEmpty(data.id));
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}

}

export class TenantData {
	select: any;
	name: string;
	typeId: number;
	address: string;
	county: string;
	town: string;
	phoneNumber: string;
	logo: string;
	taxNumber: string;
	createdDate: Date;
	taxAdministration: string
	id: number;
	actions: any;
}

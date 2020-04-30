import { Component, OnInit, ViewChild, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog, MatTableDataSource, Sort } from '@angular/material';
import { Subscription } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { LayoutUtilsService, MessageType } from '../../../core/_base/crud';
import { UserService } from '../../../services/user.service';
import { UserRegistrationEditDialogComponent } from './user-registration-edit/user-registration-edit.dialog.component';
import { PasswordChangeComponent } from './password-change/password-change.component';

@Component({
	selector: 'kt-user-registration',
	templateUrl: './user-registration.component.html'
})
export class UserRegistrationComponent implements OnInit, OnDestroy {
	locale = 'tr';
	initialData = new Array<any>();
	dataSource = new MatTableDataSource(this.initialData);

	displayedColumns = [
		"emailAddress",
		'name',
		'surname',
		'tenant',
		'id'
	];

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
		private userService: UserService
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
		this.userService.fetchAllUsers().subscribe((data: any) => {
			this.dataSource.data = data;
		});
	}

	ngOnDestroy() {
		this.subscriptions.forEach(el => el.unsubscribe());
	}

	openCreateEditDialog(isAdd: Boolean, user: any) {
		let userModel;

		if (!isAdd)
			userModel = user;
		let saveMessageTranslateParam = 'USER';
		saveMessageTranslateParam += !isAdd ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !isAdd ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(UserRegistrationEditDialogComponent, { data: { userModel } })

		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);
			let data = this.dataSource.data;

			if (isAdd) {
				data.push(res.user);
				this.dataSource.data = data;
			}
			else {
				this.dataSource.data = data.map(a => {
					if (a["id"] == res.user.id)
						return res.user;
					return a;
				});
			}

		});
	}

	changePassword(user: any) {

		const _saveMessage = this.translate.instant("CHANGE_PASSWORD");
		const _messageType = MessageType.Update;
		const dialogRef = this.dialog.open(PasswordChangeComponent, { data: { user } })

		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType);

		});
	}


	deleteUser(id) {
		// const _title: string = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.TITLE');
		// const _description: string = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.DESCRIPTION');
		// const _waitDesciption: string = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.WAIT_DESCRIPTION');
		// const _deleteMessage = this.translate.instant('ECOMMERCE.CUSTOMERS.DELETE_CUSTOMER_MULTY.MESSAGE');
		const dialogRef = this.layoutUtilsService.deleteElement("Kullanıcı silme", "Kullanıcı silinecek emin misiniz?", "Siliniyor...");
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}
			let dataSrc = this.dataSource.data;

			this.userService.deleteUserById(id).subscribe(
				data => {
					var index = dataSrc.findIndex((element) => {
						return element["id"] == data.id;
					});
					dataSrc.splice(index, 1);
					this.dataSource.data = dataSrc;
				},
				error => console.log('oops', error)
			);
			// this.layoutUtilsService.showActionNotification("Delete", MessageType.Delete);
		});
	}

	StringsOrEmpty = function( entity ) {
		return entity || "";
	};

	public getFilterString(data: any): string {
		var result: string = "";
		result = result.concat(this.StringsOrEmpty(data.name));
		result = result.concat(this.StringsOrEmpty(data.userName));
		result = result.concat(this.StringsOrEmpty(data.surname));
		result = result.concat(this.StringsOrEmpty(data.tenant ? data.tenant.name : ""));
		result = result.concat(this.StringsOrEmpty(data.emailAddress));
		result = result.concat(this.StringsOrEmpty(data.id));
		
		return (result.trim() as any).toLocaleLowerCase(this.locale);
	}

}


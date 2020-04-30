import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../../../core/core.module';
import { PartialsModule } from '../../partials/partials.module';
import { MatTableModule, MatSelectModule, MatInputModule, MatAutocompleteModule, MatRadioModule, MatNativeDateModule, MatProgressBarModule, MatDatepickerModule, MatCardModule, MatSnackBarModule, MatTabsModule, MatTooltipModule } from '@angular/material';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BundleDialogComponent } from './bundle-edit/bundle.dialog.component';
import { BundleComponent } from './bundle.component';
import { BundleService } from '../../../services/bundle.service';
import { MatSelectSearchModule } from '../../../../app/common-components/mat-select/mat-select-search.module';
import {MatToolbarModule} from '@angular/material/toolbar';
@NgModule({
	imports: [
		CommonModule,
		PartialsModule,
		CoreModule,
		MatTableModule,
		MatCheckboxModule,
		MatIconModule,
		MatMenuModule,
		MatProgressSpinnerModule,
		MatPaginatorModule,
		MatSortModule,
		MatButtonModule,
		MatMenuModule,
		MatSelectModule,
		MatInputModule,
		MatTableModule,
		MatAutocompleteModule,
		MatRadioModule,
		MatNativeDateModule,
		MatProgressBarModule,
		MatDatepickerModule,
		MatCardModule,
		MatSnackBarModule,
		MatTabsModule,
		MatTooltipModule,
		NgbProgressbarModule,
		FormsModule,
		MatToolbarModule,
		ReactiveFormsModule,
		MatSelectSearchModule,
		RouterModule.forChild([
			{
				path: '',
				component: BundleComponent
			},
		]),
	],
	entryComponents: [BundleDialogComponent],
	providers: [BundleService],
	declarations: [
		BundleComponent,
		BundleDialogComponent
	]
})
export class BundleModule {

}

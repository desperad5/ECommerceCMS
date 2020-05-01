import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatCardModule, MatDatepickerModule, MatInputModule, MatNativeDateModule, MatProgressBarModule, MatRadioModule, MatSelectModule, MatSnackBarModule, MatTableModule, MatTabsModule, MatTooltipModule } from '@angular/material';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { RouterModule } from '@angular/router';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from '../../../core/core.module';
import { PartialsModule } from '../../partials/partials.module';
import { BrandPageComponent } from './brand.component';
import { BrandEditDialogComponent } from './brand-edit/brand-edit.dialog.component';

@NgModule({
	imports: [
		CommonModule,
		PartialsModule,
		CoreModule,
		FormsModule,
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
		ReactiveFormsModule,
		RouterModule.forChild([
			{
				path: '',
				component: BrandPageComponent
			}

		]),
	],
	providers: [],
	entryComponents: [BrandEditDialogComponent],
	declarations: [
		BrandPageComponent,
		BrandEditDialogComponent
	]
})
export class BrandPageModule {
}

import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../../../core/core.module';
import { PartialsModule } from '../../partials/partials.module';
import { LessonComponent } from './lesson.component';
import { MatTableModule, MatSelectModule, MatInputModule, MatAutocompleteModule, MatRadioModule, MatNativeDateModule, MatProgressBarModule, MatDatepickerModule, MatCardModule, MatSnackBarModule, MatTabsModule, MatTooltipModule } from '@angular/material';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { LessonEditDialogComponent } from './lesson-edit/lesson-edit.dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
		ReactiveFormsModule,
		RouterModule.forChild([
			{
				path: '',
				component: LessonComponent
			},
		]),
	],
	entryComponents: [LessonEditDialogComponent],
	providers: [],
	declarations: [
		LessonComponent,
		LessonEditDialogComponent
	]
})
export class LessonModule {
}

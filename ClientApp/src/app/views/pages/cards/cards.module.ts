import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../../../core/core.module';
import { PartialsModule } from '../../partials/partials.module'; 
import { QuestionCardComponent } from './questioncard/questioncard.component';
import { ExamCardComponent } from './examcard/examcard.component';
import { MatTableModule, MatSelectModule, MatInputModule, MatAutocompleteModule, MatRadioModule, MatNativeDateModule, MatProgressBarModule, MatDatepickerModule, MatCardModule, MatSnackBarModule, MatTabsModule, MatTooltipModule } from '@angular/material';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { QuestionCardEditDialogComponent } from './questioncard/questioncard-edit/questioncard-edit.dialog.component';
import { ExamCardEditDialogComponent } from './examcard/examcard-edit/exam-edit.dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UploadComponent } from '../../partials/upload/upload.component';
import { TopicCardComponent } from './topiccard/topiccard.component';
import { TopicCardEditDialogComponent } from './topiccard/topiccard-edit/topic-edit.dialog.component';

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
				path: 'question',
				component: QuestionCardComponent
			},
			{
				path: 'exam',
				component: ExamCardComponent
			},
			{
				path: 'topic',
				component: TopicCardComponent
			}
		]),
	],
	entryComponents: [QuestionCardEditDialogComponent,UploadComponent
		,ExamCardEditDialogComponent,TopicCardEditDialogComponent],
	providers: [],
	declarations: [
		QuestionCardComponent,
		QuestionCardEditDialogComponent,
		ExamCardComponent,
		ExamCardEditDialogComponent,
		TopicCardComponent,
		TopicCardEditDialogComponent
	]
})
export class CardsModule {
}

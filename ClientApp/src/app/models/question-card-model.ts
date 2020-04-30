import { LessonModel } from './lesson-model';
import { TopicModel } from './topic-model';
import { BaseCardModel } from './base-card-model';

export class QuestionCard extends BaseCardModel {
	questionCount: number;
	lesson: LessonModel;
	topic: TopicModel;
}

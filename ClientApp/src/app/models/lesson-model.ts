import { TopicModel } from './topic-model';
import { List } from 'lodash';

export class LessonModel{
	id:number;
	name:string;
	educationLevel:number;
	createdDate:Date;
	isActive:boolean;
	topics:List<TopicModel>;	
}
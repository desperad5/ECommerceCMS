import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class TopicService {
	private headerOptions: HttpHeaders;
	headers = new HttpHeaders()
		.set('Content-Type', 'application/json')
		.set('Accept', 'application/json');

	constructor(private http: HttpClient) { }

	fetchAllActiveTopics() {
		return this.http.post("/api/Topic/FetchAllActiveTopics", {}, { headers: this.headers })
	};

	createOrEditTopic(topic: any) {
		return this.http.post<any>("/api/Topic/CreateOrEdit", topic, { headers: this.headers });
	};

	deleteTopicById(id: number) {
		return this.http.post("/api/Topic/DeleteTopicById", id, { headers: this.headers });
	};

	fetchAllLessons() {
		return this.http.post("/api/Lesson/FetchAllLessons", {}, { headers: this.headers });
	}
}

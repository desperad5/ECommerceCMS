import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICrudService } from './icrud-service';
@Injectable({
  providedIn: 'root'
})
export class QuestionCardService implements ICrudService{
  headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set('Accept', 'application/json');
            
  constructor(private http: HttpClient) { 
  }

  fetchAll() {
    return this.http.post("/api/Card/FetchAllQuestionCards", {}, { headers: this.headers })
  }
  createOrEdit(model: any) {
    return this.http.post<any>("/api/Card/CreateOrEditQuestionCard", model, { headers: this.headers });
  }
  deleteById(id: number) {
    return this.http.post<any>("/api/Card/DeleteQuestionCardById", id, { headers: this.headers });
  }
}

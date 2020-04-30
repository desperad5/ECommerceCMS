import { TestBed } from '@angular/core/testing';

import { QuestionCardService } from './questioncard.service';

describe('QuestioncardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionCardService = TestBed.get(QuestionCardService);
    expect(service).toBeTruthy();
  });
});

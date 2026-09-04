import { provideHttpClient } from '@angular/common/http';
import {
  HttpTestingController,
  provideHttpClientTesting
} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { TodoApiService } from './todo-api.service';
import { TodoItem } from './todo-item';

describe('TodoApiService', () => {
  let service: TodoApiService;
  let httpTesting: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });

    service = TestBed.inject(TodoApiService);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should get todos from the API', () => {
    const expectedTodos: TodoItem[] = [
      {
        id: '4f27093d-49de-47de-9514-c5587cc34df1',
        title: 'Buy milk',
        description: 'Buy milk for my cat',
        createdAt: '2026-09-04T03:40:37Z'
      }
    ];

    service.getTodos().subscribe(todos => {
      expect(todos).toEqual(expectedTodos);
    });

    const request = httpTesting.expectOne('/api/todos');

    expect(request.request.method).toBe('GET');

    request.flush(expectedTodos);
  });
});

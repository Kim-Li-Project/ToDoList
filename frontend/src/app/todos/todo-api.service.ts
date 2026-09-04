import { inject,  Injectable  } from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {TodoItem} from './todo-item';
import {CreateTodoRequest} from './create-todo-request';

@Injectable({
  providedIn: 'root'
})
export class TodoApiService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = '/api/todos';

  getTodos() :Observable<TodoItem[]>{
    return this.http.get<TodoItem[]>(this.apiUrl);
  }

  createTodo(request: CreateTodoRequest): Observable<TodoItem>{
    return this.http.post<TodoItem>(this.apiUrl, request)
  }

  deleteTodo(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

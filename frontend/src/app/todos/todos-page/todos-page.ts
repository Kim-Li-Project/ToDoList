import {Component, inject, OnInit, signal} from '@angular/core';
import {TodoApiService} from '../todo-api.service';
import {TodoItem} from '../todo-item';
import {TodoList} from '../todo-list/todo-list';
import {AddTodo} from '../add-todo/add-todo';

@Component({
  imports: [TodoList,AddTodo],
  selector: 'app-todos-page',
  styleUrl: './todos-page.css',
  templateUrl: './todos-page.html',
})
export class TodosPage implements OnInit {
  private readonly todoApi = inject(TodoApiService);

  protected readonly todos = signal<TodoItem[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadTodos();
  }

  private loadTodos(): void {
    this.todoApi.getTodos().subscribe({
      next: todos => {
        this.todos.set(todos);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Unable to load page. ')
        this.loading.set(false);
      }
    });
  }

  protected onDeleteRequest(id: string):void {
    this.error.set(null);

    this.todoApi.deleteTodo(id).subscribe({
      next:() => {
        this.todos.update(todos => todos.filter(todo=>todo.id !== id));
      },
      error:()=>{
        this.error.set('Unable to delete record.');
      }
    });
  }

  protected onTodoCreated(todo: TodoItem): void {
    this.todos.update(todos => [todo, ...todos]);
  }
}

import {DatePipe} from '@angular/common';
import {Component, input, output} from '@angular/core';
import {TodoItem} from '../todo-item'

@Component({
  imports: [DatePipe],
  selector: 'app-todo-list',
  styleUrl: './todo-list.css',
  templateUrl: './todo-list.html',
})
export class TodoList {
  readonly todos = input.required<TodoItem[]>();
  readonly deleteRequested = output<string>();

  protected requestDelete(id: string): void {
    this.deleteRequested.emit(id);
  }
}

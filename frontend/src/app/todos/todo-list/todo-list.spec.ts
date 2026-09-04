import { TestBed } from '@angular/core/testing';

import { TodoItem } from '../todo-item';
import { TodoList } from './todo-list';

describe('TodoList', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TodoList]
    }).compileComponents();
  });

  it('should emit id when Delete is clicked', () => {
    const todo: TodoItem = {
      id: '4f27093d-49de-47de-9514-c5587cc34df1',
      title: 'Buy milk',
      description: 'Buy milk for my cat',
      createdAt: '2026-09-04T03:40:37Z'
    };

    const fixture = TestBed.createComponent(TodoList);

    fixture.componentRef.setInput('todos', [todo]);

    let deletedId: string | undefined;

    fixture.componentInstance.deleteRequested.subscribe(id => {
      deletedId = id;
    });

    fixture.detectChanges();

    const deleteButton =
      fixture.nativeElement.querySelector('button') as HTMLButtonElement;

    deleteButton.click();

    expect(deletedId).toBe(todo.id);
  });
});

import { Component, inject, output, signal } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import{CreateTodoRequest} from '../create-todo-request';
import{TodoApiService} from '../todo-api.service';
import{TodoItem} from '../todo-item';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-add-todo',
  styleUrl: './add-todo.css',
  templateUrl: './add-todo.html',
})
export class AddTodo {
  private readonly todoApi = inject(TodoApiService);
  readonly todoCreated = output<TodoItem>();

  protected readonly formVisible = signal(false);
  protected readonly submitting = signal(false);
  protected readonly error = signal<string | null>(null);

  protected readonly form = new FormGroup({
    title: new FormControl('', {
      nonNullable:true,
      validators: [
        Validators.required,
        Validators.maxLength(100)
      ]
    }),
    description: new FormControl('', {
      nonNullable:true,
      validators: [
        Validators.maxLength(255)
      ]
    })
  });

  protected submit(): void {
    const title = this.form.controls.title.value.trim();
    const description =
      this.form.controls.description.value.trim();

    if (this.form.invalid || !title) {
      this.form.markAllAsTouched();
      return;
    }

    const request: CreateTodoRequest = {
      title,
      description: description || null
    };

    this.submitting.set(true);
    this.error.set(null);

    this.todoApi.createTodo(request).subscribe({
      next: todo => {
        this.todoCreated.emit(todo);
        this.form.reset();
        this.formVisible.set(false);
        this.submitting.set(false);
      },
      error: () => {
        this.error.set('Unable to create record.');
        this.submitting.set(false);
      }
    });
  }

  protected openForm(): void {
    this.formVisible.set(true);
  }

  protected cancel(): void {
    this.form.reset();
    this.error.set(null);
    this.formVisible.set(false);
  }

}

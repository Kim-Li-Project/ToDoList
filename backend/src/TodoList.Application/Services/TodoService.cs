using TodoList.Application.Interfaces;
using TodoList.Application.DTOs;
using TodoList.Domain.Entities;

namespace TodoList.Application.Services;

public sealed class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IReadOnlyList<TodoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var todos = await _todoRepository.GetAllAsync(cancellationToken);

        return todos.OrderByDescending(t => t.CreatedAt).Select(t => new TodoDto(t.Id, t.Title, t.Description, t.CreatedAt))
            .ToList();
    }

    public async Task<TodoDto> CreateAsync(string title, string? description,
        CancellationToken cancellationToken = default)
    {
        var todo = new Todo(
            Guid.NewGuid(),
            title,
            description,
            DateTime.UtcNow
        );
        
        await _todoRepository.AddAsync(todo, cancellationToken);

        return new TodoDto(todo.Id, todo.Title, todo.Description, todo.CreatedAt);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _todoRepository.DeleteAsync(id, cancellationToken);
    }
}
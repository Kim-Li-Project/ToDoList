using System.Collections.Concurrent;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Repositories;

public sealed class TodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, Todo> _todos = new();

    public TodoRepository()
    {
        var createdAt = DateTime.UtcNow;

        var todos = new[]
        {
            new Todo(
                Guid.NewGuid(),
                "Buy milk",
                "I need to buy milk for my cat",
                createdAt),
            new Todo(
                Guid.NewGuid(),
                "Buy eggs",
                "I need to buy eggs for my cat",
                createdAt.AddSeconds(-30)),
            new Todo(
                Guid.NewGuid(),
                "Buy bread",
                "I need to buy bread for my cat",
                createdAt.AddSeconds(-60))
        };

        foreach (var todo in todos)
        {
            _todos.TryAdd(todo.Id, todo);
        }

    }


    public Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        IReadOnlyList<Todo> todos = _todos.Values.ToList();
        
        return Task.FromResult(todos);
    }

    public Task AddAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        ArgumentNullException.ThrowIfNull(todo);

        if (!_todos.TryAdd(todo.Id, todo))
        {
            throw new InvalidOperationException("Cannot add new item");
        }

        return Task.CompletedTask;
    }
    
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return Task.FromResult(_todos.TryRemove(id, out _));
    }
}
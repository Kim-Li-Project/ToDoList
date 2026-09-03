using System.Reflection;
using TodoList.Domain.Entities;

namespace TodoList.Application.Tests.Factories;

internal static class TodoTestDataFactory
{
    public static IReadOnlyList<Todo> CreateTodos()
    {
        var todoData = new (string Title, string? Description)[]
        {
            ("Buy milk", "Buy milk for my cat"),
            ("Buy eggs", "Buy eggs for my cat"),
            ("Buy bread", "Buy bread for my cat"),
        };

        var todos = new List<Todo>();

        foreach (var item in todoData)
        {
            todos.Add(new Todo(
                Guid.NewGuid(),
                item.Title,
                item.Description,
                DateTime.UtcNow.AddDays(-todos.Count)));
        }
        
        return todos;
    }
}
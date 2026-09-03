using TodoList.Application.DTOs;

namespace TodoList.Application.Interfaces;

public interface ITodoService
{
    Task<IReadOnlyList<TodoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<TodoDto> CreateAsync(string title, string? description, CancellationToken cancellationToken = default);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
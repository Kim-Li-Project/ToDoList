using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces;

public interface ITodoRepository
{
    Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task AddAsync(Todo todo, CancellationToken cancellationToken = default);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
}
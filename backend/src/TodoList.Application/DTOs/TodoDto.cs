namespace TodoList.Application.DTOs;

public sealed record TodoDto(
    Guid Id,
    string Title,
    string? Description,
    DateTime CreatedAt
);
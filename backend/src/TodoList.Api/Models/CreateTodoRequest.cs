using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public sealed class CreateTodoRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(225)]
    public string? Description { get; set; }
}
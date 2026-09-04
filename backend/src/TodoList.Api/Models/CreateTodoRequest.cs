using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public sealed class CreateTodoRequest
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string? Description { get; set; }
}
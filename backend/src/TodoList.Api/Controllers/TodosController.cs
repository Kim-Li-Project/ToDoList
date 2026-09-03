using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Models;
using TodoList.Application.DTOs;
using TodoList.Application.Interfaces;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoDto>>> GetAll(CancellationToken cancellationToken)
    {
        var todos = await _todoService.GetAllAsync(cancellationToken);

        return Ok(todos);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create([FromBody]CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await _todoService.CreateAsync(request.Title, request.Description, cancellationToken);

        return CreatedAtAction(nameof(GetAll), todo);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _todoService.DeleteAsync(id, cancellationToken) ? Ok() : NotFound();
    }
}
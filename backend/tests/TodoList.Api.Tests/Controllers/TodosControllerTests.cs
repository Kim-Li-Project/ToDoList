using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoList.Api.Models;
using TodoList.Application.DTOs;

namespace TodoList.Api.Tests.Controllers;

public class TodosControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TodosControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll()
    {
        var response = await _client.GetAsync("/api/todos");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var todos = await response.Content.ReadFromJsonAsync<List<TodoDto>>();

        Assert.NotNull(todos);
        Assert.NotEmpty(todos);
        Assert.Equal(3, todos.Count);
        Assert.Equal(todos.OrderByDescending(t => t.CreatedAt).First().CreatedAt, todos[0].CreatedAt);
    }

    [Fact]
    public async Task Create_WithValidData()
    {
        var request = new CreateTodoRequest
        {
            Title = "Buy milk",
            Description = "Buy milk for my cat"
        };

        var response = await _client.PostAsJsonAsync("/api/todos", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var createdTodo = await response.Content.ReadFromJsonAsync<TodoDto>();

        Assert.NotNull(createdTodo);
        Assert.Equal(request.Title, createdTodo.Title);
        Assert.Equal(request.Description, createdTodo.Description);
    }

    [Fact]
    public async Task Create_WithInvalidData()
    {
        var request = new CreateTodoRequest();

        var response = await _client.PostAsJsonAsync("/api/todos", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_WithValidId()
    {
        var request = new CreateTodoRequest
        {
            Title = "Todo to delete",
            Description = null
        };

        var createResponse = await _client.PostAsJsonAsync("/api/todos", request);

        var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoDto>();

        Assert.NotNull(createdTodo);

        var deleteResponse = await _client.DeleteAsync($"/api/todos/{createdTodo!.Id}");

        Assert.Equal(
            HttpStatusCode.NoContent,
            deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WithInvalidId()
    {
        var invalidId = Guid.NewGuid();
        
        var response = await _client.DeleteAsync($"/api/todos/{invalidId}");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
    }
}
using System.Runtime.InteropServices;
using Moq;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Application.Tests.Factories;
using TodoList.Domain.Entities;

namespace TodoList.Application.Tests.Services;

public class TodoServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllTodos()
    {
        var todos = TodoTestDataFactory.CreateTodos();

        var repositoryMock = new Mock<ITodoRepository>();

        repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(todos);

        var service = new TodoService(repositoryMock.Object);

        var result = await service.GetAllAsync();

        Assert.Equal(todos.Count, result.Count);
        Assert.Equal("Buy milk", result[0].Title);
        Assert.Equal("Buy eggs", result[1].Title);
        Assert.Equal("Buy bread", result[2].Title);

        repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ReturnsTodo()
    {
        var repositoryMock = new Mock<ITodoRepository>();

        repositoryMock.Setup(r => r.AddAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new TodoService(repositoryMock.Object);

        var result = await service.CreateAsync("Buy food", "Buy food for my cat");

        Assert.Equal("Buy food", result.Title);
        Assert.Equal("Buy food for my cat", result.Description);
        Assert.Equal(DateTimeKind.Utc, result.CreatedAt.Kind);

        repositoryMock.Verify(
            r => r.AddAsync(
                It.Is<Todo>(t => t.Id == result.Id && t.Title == "Buy food" && t.Description == "Buy food for my cat"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public async Task CreateAsync_WithInvalidTitle_ThrowsException(string? title)
    {
        var repositoryMock = new Mock<ITodoRepository>();
        var service = new TodoService(repositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(title!, null));

        repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteAsync_ReturnsRepositoryResult(bool result)
    {
        var id = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoRepository>();
        
        repositoryMock.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(result);
        
        var service = new TodoService(repositoryMock.Object);
        
        var actual = await service.DeleteAsync(id);
        
        Assert.Equal(result, actual);
        
    }
}
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using AutoMockFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using ProgerTasks.Controllers.V1;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;
using ProgerTasks.Service.Implementations.Tasks;
using ProgerTasks.Service.Interfaces.Tasks;
using ProgrammerProduct.Tests;

namespace ProgerTasks.Tests.UnitTests;

public class TasksControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        var mockTaskService = new Mock<ITaskService>();
        
        var taskService = mockTaskService.As<ITaskService>().Object;
        
        _controller = new TasksController(taskService);
    }
    
    [Fact]
    public async Task CreateTask_ShouldReturnOkStatusCode()
    {
        // Arrange
        var taskInput = new TaskInput()
        {
            Title = "abc",
            Author = "dfkngjkd",
            Description = "dfjgjdfgjdfkbg",
            TeamName = "djfkgdkfjdkg",
            Priority = Priority.Hard
        };

        // Act
        var response = await _controller.CreateTask(taskInput);

        // Assert
        response.StatusCode.ToString().Should()
            .Be(HttpStatusCode.OK.ToString());
    }

    [Fact]
    public async Task CreateTask_ShouldReturnTaskEntity()
    {
        // Arrange
        var taskInput = new TaskInput()
        {
            Title = "abc",
            Author = "dfkngjkd",
            Description = "dfjgjdfgjdfkbg",
            TeamName = "djfkgdkfjdkg",
            Priority = Priority.Hard
        };

        // Act
        var response = await _controller.CreateTask(taskInput);

        // Assert
        response.Data.Should()
            .BeOfType<TaskEntity>();
        response.Data.Title.Should()
            .Be(taskInput.Title);
        response.Data.Description.Should()
            .Be(taskInput.Description);
    }
}
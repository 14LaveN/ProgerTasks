using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProgerTasks.Controllers.V1;
using ProgerTasks.DAL;
using ProgerTasks.DAL.Interfaces;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;
using ProgerTasks.Domain.Response;
using ProgerTasks.Service.Implementations.Tasks;
using ProgerTasks.Service.Interfaces.Tasks;

namespace ProgerTasks.TestProject;

public class Tests
{
    private TasksController _taskController;
    private ITaskService _mockTaskService;
    private TaskInput taskInput;

    [SetUp]
    public void SetUp()
    {
        taskInput = new TaskInput
        {
            Title = "abc",
            Author = "dfkngjkd",
            Description = "dfjgjdfgjdfkbg",
            TeamName = "djfkgdkfjdkg",
            Priority = Priority.Hard
        };
        _taskController = new TasksController(_mockTaskService);
    }

    [Test]
    public async Task CreateTask_ShouldReturnOkResponse()
    {
        // Act
        var result = await _taskController.CreateTask(taskInput);

        // Assert
        Assert.That(result, Is.TypeOf<IBaseResponse<TaskEntity>>());
        Assert.That(result.StatusCode, Is.EqualTo(Domain.Enum.StatusCode.Ok));
    }
}
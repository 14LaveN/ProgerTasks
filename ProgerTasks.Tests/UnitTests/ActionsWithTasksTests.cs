using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMockFixture;
using Castle.Core.Configuration;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using ProgerTasks.Controllers.V1;
using ProgerTasks.DAL;
using ProgerTasks.DAL.Interfaces;
using ProgerTasks.DAL.Repositories.Tasks;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;
using ProgerTasks.Service.Implementations.Tasks;
using ProgerTasks.Service.Interfaces.Tasks;
using ProgrammerProduct.Tests;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ProgerTasks.Tests.UnitTests;

public class ActionsWithTasksTests
    {
         private readonly TaskRepository _taskRepository;
         private readonly AppDbContext _appDbContext;

        public ActionsWithTasksTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            //var mockDbContext = fixture.Create<AppDbContext>();
            _appDbContext = new AppDbContext();
            _taskRepository = new TaskRepository(_appDbContext);
        }

        [Fact]
        public async Task GetAllTasks_ReturnsAllTasks()
        {
            // Arrange

            // Act
            var actualTaskEntities = await _taskRepository.GetAllTasks();

            // Assert
            actualTaskEntities.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByTeamNameAndTitle_ReturnsTask()
        {
            // Arrange
            var taskTeamName = "Team 1";
            var taskTitle = "Task 1";
            
            // Act
            var actualTaskEntity = await _taskRepository.GetByTeamNameAndTitle(taskTeamName, taskTitle);

            // Assert
            Assert.Equal(taskTeamName, actualTaskEntity.TeamName);
            Assert.Equal(taskTitle, actualTaskEntity.Title);
        }

        [Fact]
        public async Task CreateTask_CreatesTask()
        {
            // Arrange
            var taskEntity = new TaskEntity { Author = "Alice", Title = "Task 1", Description = "Description 1", TeamName = "Team 1", Priority = Priority.Easy, CreationDate = DateTime.Now, CommentsCount = 0};

            // Act
            await _taskRepository.CreateTask(taskEntity);
            var task =  await _taskRepository.GetByTeamNameAndTitle(taskEntity.TeamName, taskEntity.Title);

            // Assert
            Assert.Equal(taskEntity.TeamName, task.TeamName);
            Assert.Equal(taskEntity.Title, task.Title);
        }

        [Fact]
        public async Task UpdateTask_UpdatesTask()
        {
            // Arrange
            var taskEntity = new TaskEntity { Id = 1, Author = "Alice", Title = "Task 1", Description = "Description 1", TeamName = "Team 1", Priority = Priority.Easy, CreationDate = DateTime.Now };

            // Act
            await _taskRepository.UpdateTask(taskEntity.Id, taskEntity);

            // Assert
            // TODO: Assert that the task was updated in the database.
        }

        [Fact]
        public async Task DeleteTask_DeletesTask()
        {
            // Arrange
            var taskEntityId = 1;

            // Act
            await _taskRepository.DeleteTask(taskEntityId);

            // Assert
            // TODO: Assert that the task was deleted from the database.
        }

        [Fact]
        public async Task GetById_ReturnsTask()
        {
            // Arrange
            int id = 5;

            // Act
            var actualTaskEntity = await _taskRepository.GetById(id);

            // Assert
            actualTaskEntity.Should().NotBeNull();
        }
    }
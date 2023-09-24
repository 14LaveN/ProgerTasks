using FluentValidation;
using Microsoft.Extensions.Logging;
using ProgerTasks.DAL.Interfaces;
using ProgerTasks.DAL.Interfaces.Tasks;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;
using ProgerTasks.Domain.Response;
using ProgerTasks.Service.Interfaces.Tasks;

namespace ProgerTasks.Service.Implementations.Tasks;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TaskService> _logger;
    private readonly IValidator<TaskInput> _validator;

    public TaskService(IUnitOfWork unitOfWork,
        ILogger<TaskService> logger,
        IValidator<TaskInput> validator) =>
        (_logger, _unitOfWork, _validator) =
            (logger, unitOfWork, validator);

    public async Task<IBaseResponse<TaskEntity>> CreateTask(TaskInput taskInput)
    {
        try
        {
            _logger.LogInformation($"Request for create a task - {taskInput.Title}");

            var errors = await _validator.ValidateAsync(taskInput);

            if (errors.Errors.Count is not 0)
            {
                throw new AggregateException($"{errors.Errors}");
            }
            
            var task = await _unitOfWork.TaskRepository.GetByTeamNameAndTitle(taskInput.TeamName, taskInput.Title);

            if (task is not null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name already taken",
                    StatusCode = StatusCode.NotFound
                };
            }

            task = taskInput;

            await _unitOfWork.TaskRepository.CreateTask(task);

            _logger.LogInformation($"Task created: {task.Title}");
            return new BaseResponse<TaskEntity>()
            {
                Data = task,
                Description = "Task created",
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[TaskService.CreateTask]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<TaskEntity>> DeleteTask(int id)
    {
        try
        {
            var task = await _unitOfWork.TaskRepository.GetById(id);
            
            _logger.LogInformation($"Request for delete a task - {task.Title}");

            if (task is null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            await _unitOfWork.TaskRepository.DeleteTask(id);

            _logger.LogInformation($"Task deleted: {task.Title}");
            return new BaseResponse<TaskEntity>()
            {
                Data = task,
                Description = "Task deleted",
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[TaskService.DeleteTask]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<TaskEntity>>> GetAllTasks()
    {
        try
        {
            _logger.LogInformation($"Request for get all tasks");
            var tasks = await _unitOfWork.TaskRepository.GetAllTasks();
            
            if (tasks is null)
            {
                return new BaseResponse<IEnumerable<TaskEntity>>()
                {
                    Description = "Tasks not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            _logger.LogInformation($"Tasks received");
            return new BaseResponse<IEnumerable<TaskEntity>>()
            {
                Data = tasks,
                Description = "Tasks received",
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[TaskService.GetAllTasks]: {exception.Message}");
            return new BaseResponse<IEnumerable<TaskEntity>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<TaskEntity>> UpdateTask(int id, TaskInput taskInput)
    {
        try
        {
            _logger.LogInformation($"Request for update a task - {taskInput.Title}");

            var task = await _unitOfWork.TaskRepository.GetById(id);

            var errors = await _validator.ValidateAsync(taskInput);

            if (errors.Errors.Count is not 0)
            {
                throw new AggregateException($"{errors.Errors}");
            }
            
            if (task is null) 
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Task with the same name not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            task = taskInput;
            task.Id = id;

            await _unitOfWork.TaskRepository.UpdateTask(id, task);

            _logger.LogInformation($"Task updated: {task.Title}");
            return new BaseResponse<TaskEntity>()
            {
                Data = task,
                Description = "Task updated",
                StatusCode = StatusCode.Ok
            };

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[TaskService.UpdateTask]: {exception.Message}");
            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
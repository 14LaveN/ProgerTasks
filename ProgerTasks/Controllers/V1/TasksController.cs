using Microsoft.AspNetCore.Mvc;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Response;
using ProgerTasks.Service.Interfaces.Tasks;

namespace ProgerTasks.Controllers.V1;

[ApiController]
[Route("api/v1/tasks")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "v1")]
public sealed class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Create task
    /// </summary>
    /// <param name="taskInput"></param>
    /// <returns>Base information about task creating</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Returns task</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    /// 
    
    [HttpPost]
    public async Task<IBaseResponse<TaskEntity>> CreateTask([FromBody] TaskInput taskInput)
    {
        var response = await _taskService.CreateTask(taskInput);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return response;
        
        throw new AggregateException(nameof(response));
    }
    
    /// <summary>
    /// Get all tasks
    /// </summary>
    /// <param name="taskInput"></param>
    /// <returns>Base information about get all tasks</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Returns tasks</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    /// 

    [HttpGet]
    public async Task<IBaseResponse<IEnumerable<TaskEntity>>> GetAllTasks()
    {
        var response = await _taskService.GetAllTasks();
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return response;
        
        throw new AggregateException(nameof(response));
    }

    /// <summary>
    /// Update task
    /// </summary>
    /// <param name="id"></param>
    /// <param name="taskInput"></param>
    /// <returns>Base information about updating task</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return updated task</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
     
    [HttpPut]
    public async Task<IBaseResponse<TaskEntity>> UpdateTask(int id, [FromBody] TaskInput taskInput)
    {
        var response = await _taskService.UpdateTask(id, taskInput);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return response;
        
        throw new AggregateException(nameof(response));
    }

    /// <summary>
    /// Delete task
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Base information about updating task</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return deleted task</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpDelete]
    public async Task<IBaseResponse<TaskEntity>> DeleteTask(int id)
    {
        var response = await _taskService.DeleteTask(id);
        
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return response;

        throw new AggregateException(nameof(response));
    }
}
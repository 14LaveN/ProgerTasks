using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Response;

namespace ProgerTasks.Service.Interfaces.Tasks;

public interface ITaskService
{
    Task<IBaseResponse<TaskEntity>> CreateTask(TaskInput taskInput);
    
    Task<IBaseResponse<TaskEntity>> DeleteTask(int id);
    
    Task<IBaseResponse<IEnumerable<TaskEntity>>> GetAllTasks();
    
    Task<IBaseResponse<TaskEntity>> UpdateTask(int id, TaskInput taskInput);
}
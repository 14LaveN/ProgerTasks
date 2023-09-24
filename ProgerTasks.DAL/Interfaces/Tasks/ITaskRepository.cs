using ProgerTasks.Domain.Entity;

namespace ProgerTasks.DAL.Interfaces.Tasks;

public interface ITaskRepository
{
    Task<TaskEntity> CreateTask(TaskEntity taskEntity);

    Task DeleteTask(int id);

    Task<TaskEntity> UpdateTask(int id, TaskEntity taskEntity);

    Task<TaskEntity> GetByTeamNameAndTitle(string teamName, string title);

    Task<IEnumerable<TaskEntity>> GetAllTasks();

    Task<TaskEntity> GetById(int id);
}
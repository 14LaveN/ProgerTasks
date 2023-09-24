using Dapper;
using ProgerTasks.DAL.Interfaces.Tasks;
using ProgerTasks.Domain.Entity;

namespace ProgerTasks.DAL.Repositories.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _appDbContext;

    public TaskRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<IEnumerable<TaskEntity>> GetAllTasks()
    {
        using (var conn = _appDbContext.CreateConnection())
        {
            conn.Open();
            return await conn.QueryAsync<TaskEntity>("SELECT * FROM tasks");
        }
    }

    public async Task<TaskEntity> GetByTeamNameAndTitle(string teamName, string title)
    {
        using var conn = _appDbContext.CreateConnection();
        
        conn.Open();
        return await conn.QueryFirstOrDefaultAsync<TaskEntity>("SELECT * FROM tasks WHERE TeamName = @TeamName AND Title = @Title", new { TeamName = teamName, Title = title });
    }
    
    public async Task<TaskEntity> CreateTask(TaskEntity taskEntity)
    {
        using var conn = _appDbContext.CreateConnection();
        
        conn.Open();
        return await conn.QueryFirstOrDefaultAsync(
            "INSERT INTO tasks (Author, Title, Description, TeamName, Priority, CreationDate) " +
                                                   "VALUES(@Author, @Title, @Description, @TeamName, @Priority, @CreationDate)", taskEntity);
    }
    
    public async Task<TaskEntity> UpdateTask(int id, TaskEntity taskEntity)
    {
        using var conn = _appDbContext.CreateConnection();

        conn.Open();
        
        using var transaction = conn.BeginTransaction();

        try
        {
            var task =  await conn.QueryFirstOrDefaultAsync<TaskEntity>("UPDATE tasks SET Author = @Author," +
                                                                        " Title = @Title," +
                                                                        " Description = @Description," +
                                                                        "TeamName =  @TeamName," +
                                                                        "Priority = @Priority," +
                                                                        "CreationDate = @CreationDate " +
                                                                        "WHERE Id = @Id", taskEntity );
            
            transaction.Commit();
            return task;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
    
    public async Task DeleteTask(int id)
    {
        using var conn = _appDbContext.CreateConnection();
        conn.Open();
        await conn.QueryFirstOrDefaultAsync<TaskEntity>("DELETE FROM tasks WHERE Id = @Id", new { Id = id });
    }

    public async Task<TaskEntity> GetById(int id)
    {
        using (var conn = _appDbContext.CreateConnection())
        {
            conn.Open();
            return await conn.QueryFirstOrDefaultAsync<TaskEntity>("SELECT * FROM tasks WHERE Id = @Id", new { Id = id });
        }
    }
}
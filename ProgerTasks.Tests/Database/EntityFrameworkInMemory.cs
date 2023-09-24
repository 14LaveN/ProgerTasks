using Dapper;
using ProgerTasks.DAL;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Tests.Database;

public static class EntityFrameworkInMemory
{
    public static void InitializeRange(AppDbContext appDbContext)
    {
        var tasks = SeedTaskEntities();
        using var conn = appDbContext.CreateConnection();
        
        conn.Open();
        var firstRequest = conn.QueryFirstOrDefault("INSERT INTO tasks (Author, Title, Description, TeamName, Priority, CreationDate)" 
                                                    +"VALUES(@Author, @Title, @Description, @TeamName, @Priority, @CreationDate),", tasks.First());
        
        var secondRequest = conn.QueryFirstOrDefault("INSERT INTO tasks (Author, Title, Description, TeamName, Priority, CreationDate)" 
                                                    +"VALUES(@Author, @Title, @Description, @TeamName, @Priority, @CreationDate),", tasks
                                                    .First(x =>x.Title == "abcd"));
        
        var thirdRequest = conn.QueryFirstOrDefault("INSERT INTO tasks (Author, Title, Description, TeamName, Priority, CreationDate)" 
                                                     +"VALUES(@Author, @Title, @Description, @TeamName, @Priority, @CreationDate),", tasks
                                                     .First(x =>x.Title == "abcdef"));
    }
    
    public static void InitializeByDTO(AppDbContext appDbContext)
    {
        var taskInput = SeedTaskDTO().First(x=> x.Title == "abc");
        TaskEntity task = taskInput;
        
        using var conn = appDbContext.CreateConnection();
        
        conn.Open();
        var firstRequest = conn.QueryFirstOrDefault("INSERT INTO tasks (Author, Title, Description, TeamName, Priority, CreationDate)" 
                                                    +"VALUES(@Author, @Title, @Description, @TeamName, @Priority, @CreationDate),", task);
    }
    
    public static void ReInitializeByEntity(AppDbContext appDbContext)
    {
        var tasks = SeedTaskEntities();
        using var conn = appDbContext.CreateConnection();
        
        conn.Open();
        var request = conn.QueryFirstOrDefault("DELETE FROM tasks WHERE Id = @Id AND @IdSecond AND @IdThird",
            new { Id = tasks.First().Id,
            IdSecond = tasks.First(x=>x.Title=="abcd").Id,
            IdThird = tasks.Last().Id });
    }

    public static List<TaskInput> SeedTaskDTO()
    {
        return new List<TaskInput>
        {
            new TaskInput()
            {
                Title = "abc",
                Author = "dfkngjkd",
                Description = "dfjgjdfgjdfkbg",
                TeamName = "djfkgdkfjdkg",
                Priority = Priority.Hard
            },
            new TaskInput
            {
                Title = "abcd",
                Author = "dfkngjkd",
                Description = "dfjgjdfgjdfkbg",
                TeamName = "djfkgdkfjdkg",
                Priority = Priority.Easy
            },
        };
    }

    public static List<TaskEntity> SeedTaskEntities()
    {
        return new List<TaskEntity>
        {
            new TaskEntity
            {  
                Title = "abc",
                Author =   "dfkngjkd",
                Description = "dfjgjdfgjdfkbg",
                CreationDate = DateTime.Now,
                Priority = Priority.Easy,
                TeamName = "djfkgdkfjdkg"
            },
            new TaskEntity
            {  
                Title = "abcd",
                Author =   "dfkngjkd",
                Description = "dfjgjdfgjdfkbg",
                CreationDate = DateTime.Now,
                Priority = Priority.Easy,
                TeamName = "djfkgdkfjdkg"
            },
            new TaskEntity
            {  
                Title = "abcdef",
                Author =   "dfknafdsgjkd",
                Description = "dfjgjdfgjdfkbg",
                CreationDate = DateTime.Now,
                Priority = Priority.Easy,
                TeamName = "djfkgdkfjdkg"
            }
        };
    }
}
using System.ComponentModel.DataAnnotations;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.Entity;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    public string Author { get; set; }
    
    public string TeamName { get; set; }
    
    public string Description { get; set; }
    
    public string Title { get; set; }
    
    public int? CommentsCount { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public Priority  Priority { get; set; }
    
    
    public static implicit operator TaskEntity(TaskInput taskInput)
    {
        return new TaskEntity()
        {
            TeamName = taskInput.TeamName,
            Author = taskInput.Author,
            Description = taskInput.Description,
            Title = taskInput.Title,
            CreationDate = DateTime.Now,
            Priority = taskInput.Priority
        };
    }
}
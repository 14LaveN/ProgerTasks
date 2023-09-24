using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.DTO.Task;

public class TaskOutput
{

    public required string CommentsCount { get; set; }

    public required string Author { get; set; }
    
    public required string TeamName { get; set; }
    
    public required string Description { get; set; }
    
    public required string Title { get; set; }
    
    public required DateTime CreationDate { get; set; }
    
    public required Priority  Priority { get; set; }
}
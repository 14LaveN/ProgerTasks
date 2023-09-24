using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.DTO.Task;

public class TaskInput
{
    public required string Author { get; set; }
    
    public required string Description { get; set; }

    public required string TeamName { get; set; }

    public required string Title { get; set; }
    
    public required Priority  Priority { get; set; }
}
using System.ComponentModel.DataAnnotations;
using ProgerTasks.Domain.DTO.Account;
using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.Entity;

public class AccountEntity
{
    [Key] public int Id { get; set; }

    public string Author { get; set; }  = null!;

    public string TeamName { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public TeamRoles TeamRole {get; set; }
    
    public string Description { get; set; }  = null!;

    public Roles Role { get; set; }

    public int? TasksCount { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public static implicit operator AccountEntity(AccountInput accountInput)
    {
        return new AccountEntity()
        {
            TeamName = accountInput.TeamName,
            Author = accountInput.Author,
            Role = Roles.User,
            Password = accountInput.Password,
            Description = accountInput.Description,
            TeamRole = accountInput.TeamRole,
            CreationDate = DateTime.Now,
            TasksCount = 0
        };
    }
}
using System.ComponentModel.DataAnnotations;
using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.DTO.Account;

public class AccountInput
{
    public required string Author { get; set; }  = null!;

    public required string Password { get; set; } = null!;

    [Compare("Password")]
    public required string PasswordConfirm { get; set; } = null!;

    public required string TeamName { get; set; } = null!;

    public required TeamRoles TeamRole {get; set; }
    
    public required string Description { get; set; }  = null!;
}
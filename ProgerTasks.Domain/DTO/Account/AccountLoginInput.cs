namespace ProgerTasks.Domain.DTO.Account;

public class AccountLoginInput
{
    public string Author { get; set; }  = null!;

    public string Password { get; set; } = null!;

    public string TeamName { get; set; } = null!;
}
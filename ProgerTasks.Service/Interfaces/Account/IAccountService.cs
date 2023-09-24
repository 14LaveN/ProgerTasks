using System.Security.Claims;
using ProgerTasks.Domain.DTO.Account;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Response;

namespace ProgerTasks.Service.Interfaces.Account;

public interface IAccountService
{
    Task<IBaseResponse<ClaimsIdentity>> LoginAccount(AccountLoginInput accountLoginInput);

    Task<IBaseResponse<ClaimsIdentity>> RegisterAccount(AccountInput accountInput);

    Task<IBaseResponse<string>> ChangePassword(string newPassword, int id);
    
    Task<IBaseResponse<string>> ChangeAuthor(int id, string newTeamName);
}
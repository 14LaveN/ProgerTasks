using ProgerTasks.Domain.Entity;

namespace ProgerTasks.DAL.Interfaces.Account;

public interface IAccountRepository
{
    Task<AccountEntity> CreateAccount(AccountEntity accountEntity);

    Task DeleteAccount(int id);

    Task<AccountEntity> UpdateAccount(int id, AccountEntity accountEntity);

    Task<AccountEntity> UpdateAccountPassword(int id, string newPassword);

    Task<AccountEntity> UpdateAccountTeamName(int id, string newTeamName);

    Task<AccountEntity> GetByTeamNameAndAuthor(string teamName, string title);

    Task<IEnumerable<AccountEntity>> GetAllAccounts();

    Task<AccountEntity> GetById(int id);
}
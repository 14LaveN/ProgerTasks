using Dapper;
using ProgerTasks.DAL.Interfaces.Account;
using ProgerTasks.Domain.Entity;

namespace ProgerTasks.DAL.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _appDbContext;

    public AccountRepository(AppDbContext appDbContext) =>
        _appDbContext = appDbContext;
    
    public async Task<IEnumerable<AccountEntity>> GetAllAccounts()
    {
        using var conn = _appDbContext.CreateConnection();
        
        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            var account = await conn.QueryAsync<AccountEntity>("SELECT * FROM accounts");
            transaction.Commit();

            return account;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<AccountEntity> GetByTeamNameAndAuthor(string teamName, string author)
    {
        using var conn = _appDbContext.CreateConnection();

        conn.Open();
        
        using var transaction = conn.BeginTransaction();
        
        try
        {

            //await conn.ExecuteAsync("CREATE INDEX idx_account_teamname_author ON accounts (teamName, author);");
            
            var accountEntity = await conn.QueryFirstOrDefaultAsync<AccountEntity>("SELECT * FROM accounts WHERE TeamName = @TeamName" +
                " AND Author = @Author", new { TeamName = teamName, Author = author });

            
            transaction.Commit();

            return accountEntity;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<AccountEntity> CreateAccount(AccountEntity accountEntity)
    {
        using var conn = _appDbContext.CreateConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            var account = await conn.QueryFirstOrDefaultAsync(
                "INSERT INTO accounts (Author, Description, TeamName, CreationDate, TasksCount, TeamRole, Password, Role) " +
                "VALUES(@Author, @Description, @TeamName, @CreationDate, @TasksCount, @TeamRole, @Password, @Role)",
                accountEntity);
            transaction.Commit();
    
            return accountEntity;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }
    
    public async Task<AccountEntity> UpdateAccount(int id, AccountEntity accountEntity)
    {
        using var conn = _appDbContext.CreateConnection();
        
        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            var account = await conn.QueryFirstOrDefaultAsync<AccountEntity>("UPDATE accounts SET Author = @Author," +
                                                                             " Description = @Description," +
                                                                             "TeamName =  @TeamName," +
                                                                             "CreationDate = @CreationDate," +
                                                                             "TeamRole = @TeamRole," +
                                                                             "Password = @Password," +
                                                                             "Role = @Role," +
                                                                             "TasksCount = @TasksCount," +
                                                                             "CreationDate = @CreationDate " +
                                                                             "WHERE Id = @Id", accountEntity);
            transaction.Commit();
            return account;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<AccountEntity> UpdateAccountPassword(int id, string newPassword)
    {
        using var conn = _appDbContext.CreateConnection();

        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            var account = await conn.QueryFirstOrDefaultAsync<AccountEntity>("UPDATE accounts SET Password = @Password " +
                                                                  "WHERE Id = @Id",
            new { Id = id, Password = newPassword });
        transaction.Commit();
        return account;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }
    
    public async Task<AccountEntity> UpdateAccountTeamName(int id, string newTeamName)
    {
        using var conn = _appDbContext.CreateConnection();

        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            var account = await conn.QueryFirstOrDefaultAsync<AccountEntity>("UPDATE accounts SET TeamName = @TeamName " +
                                                                  "WHERE Id = @Id",
            new { Id = id, TeamName = newTeamName });
        transaction.Commit();
        return account;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task DeleteAccount(int id)
    {
        using var conn = _appDbContext.CreateConnection();
        
        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            var account = await conn.QueryFirstOrDefaultAsync<AccountEntity>("DELETE FROM accounts WHERE Id = @Id", new { Id = id });
            transaction.Commit();
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<AccountEntity> GetById(int id)
    {
        using var conn = _appDbContext.CreateConnection();
        
        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            //await conn.ExecuteAsync("CREATE INDEX idx_account_id ON accounts (id);");
            
            var account = await conn.QueryFirstOrDefaultAsync<AccountEntity>("SELECT * FROM accounts WHERE Id = @Id", new { Id = id });
        
        transaction.Commit();
        return account;
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}
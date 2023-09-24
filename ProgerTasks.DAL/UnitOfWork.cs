using ProgerTasks.DAL.Interfaces;
using ProgerTasks.DAL.Interfaces.Account;
using ProgerTasks.DAL.Interfaces.Tasks;
using ProgerTasks.DAL.Repositories.Account;
using ProgerTasks.DAL.Repositories.Tasks;

namespace ProgerTasks.DAL;

public class UnitOfWork : IUnitOfWork
{
    private IAccountRepository? _accountRepository;
    private ITaskRepository? _taskRepository;
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(IAccountRepository accountRepository,
        ITaskRepository taskRepository,
        AppDbContext appDbContext)
    {
        _accountRepository = accountRepository;
        _taskRepository = taskRepository;
        _appDbContext = appDbContext;
    }

    public IAccountRepository AccountRepository
    {
        get
        {
            if (_accountRepository is null)
                _accountRepository = new AccountRepository(_appDbContext);
            return _accountRepository;
        }
    }
    
    public ITaskRepository TaskRepository
    {
        get
        {
            if (_taskRepository is null)
                _taskRepository = new TaskRepository(_appDbContext);
            return _taskRepository;
        }
    }
}
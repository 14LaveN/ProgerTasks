using ProgerTasks.DAL.Interfaces.Account;
using ProgerTasks.DAL.Interfaces.Tasks;

namespace ProgerTasks.DAL.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    ITaskRepository TaskRepository { get; }
}
using Microsoft.Extensions.DependencyInjection;
using ProgerTasks.DAL.Interfaces;
using ProgerTasks.DAL.Interfaces.Account;
using ProgerTasks.DAL.Interfaces.Tasks;
using ProgerTasks.DAL.Repositories.Account;
using ProgerTasks.DAL.Repositories.Tasks;

namespace ProgerTasks.DAL;

public static class EntryDatabase
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
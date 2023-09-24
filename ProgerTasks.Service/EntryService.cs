using Microsoft.Extensions.DependencyInjection;
using ProgerTasks.Service.Implementations.Account;
using ProgerTasks.Service.Implementations.Tasks;
using ProgerTasks.Service.Interfaces.Account;
using ProgerTasks.Service.Interfaces.Tasks;

namespace ProgerTasks.Service;

public static class EntryService
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProgerTasks.Domain.DTO.Account;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Validators;

namespace ProgerTasks.Domain;

public static class EntryDto
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<IValidator<TaskInput>, TaskDtoValidator>();
        services.AddScoped<IValidator<AccountInput>, AccountDtoValidator>();

        return services;
    }
}
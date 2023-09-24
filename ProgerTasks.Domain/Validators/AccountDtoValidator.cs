using FluentValidation;
using ProgerTasks.Domain.DTO.Account;
using ProgerTasks.Domain.DTO.Task;
using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.Validators;

public class AccountDtoValidator : AbstractValidator<AccountInput>
{
    public AccountDtoValidator()
    {
        RuleFor(accountInput =>
                accountInput.Author).NotEmpty()
            .MaximumLength(25)
            .WithMessage("Your author name too big");
        
        RuleFor(accountInput =>
                accountInput.TeamRole).NotEmpty()
            .WithMessage("You don't have a team");
        
        RuleFor(accountInput =>
                accountInput.Description).NotEmpty()
            .MaximumLength(250)
            .WithMessage("Your description too big");
        
        RuleFor(accountInput =>
                accountInput.Password).NotEmpty()
            .MaximumLength(24)
            .WithMessage("Your password is too big");
        
        RuleFor(accountInput =>
                accountInput.TeamName).NotEmpty()
            .MaximumLength(16)
            .WithMessage("You don't have a team");
    }
}
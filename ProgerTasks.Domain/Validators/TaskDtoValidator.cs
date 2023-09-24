using FluentValidation;
using ProgerTasks.Domain.DTO.Task;

namespace ProgerTasks.Domain.Validators;

public class TaskDtoValidator : AbstractValidator<TaskInput>
{
    public TaskDtoValidator()
    {
        RuleFor(taskInput =>
                taskInput.Author).NotEmpty()
            .MaximumLength(25)
            .WithMessage("Your author name too big");
        
        RuleFor(taskInput =>
                taskInput.Priority).NotEmpty()
            .WithMessage("Priority was null");
        
        RuleFor(taskInput =>
                taskInput.Description).NotEmpty()
            .MaximumLength(250)
            .WithMessage("Your description too big");
        
        RuleFor(taskInput =>
                taskInput.Title).NotEmpty()
            .MaximumLength(24)
            .WithMessage("Your title is too big");
        
        RuleFor(taskInput =>
                taskInput.TeamName).NotEmpty()
            .MaximumLength(16)
            .WithMessage("Your team name too big");
    }
}
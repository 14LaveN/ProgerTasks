using System.Security.Claims;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.Excel;
using ProgerTasks.DAL.Interfaces;
using ProgerTasks.DAL.Interfaces.Account;
using ProgerTasks.Domain.DTO.Account;
using ProgerTasks.Domain.Entity;
using ProgerTasks.Domain.Enum;
using ProgerTasks.Domain.Helpers;
using ProgerTasks.Domain.Response;
using ProgerTasks.Service.Interfaces.Account;

namespace ProgerTasks.Service.Implementations.Account;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IValidator<AccountInput> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(ILogger<AccountService> logger,
        IUnitOfWork unitOfWork,
        IValidator<AccountInput> validator) =>
            (_logger, _unitOfWork, _validator) =
            (logger, unitOfWork, validator);

    public async Task<IBaseResponse<ClaimsIdentity>> LoginAccount(AccountLoginInput accountLoginInput)
    {
        try
        {
            _logger.LogInformation($"Request for login an account - {accountLoginInput.Author}");
            
            var account = await _unitOfWork.AccountRepository.GetByTeamNameAndAuthor(accountLoginInput.TeamName, accountLoginInput.Author);
            
            if (account is null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Account with the same name not found",
                };
            }
            
            if (account.Password != HashPasswordHelper.HashPassowrd(accountLoginInput.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Incorrect password"
                };
            }

            var result = Authenticate(account);

            _logger.LogInformation($"Login account - {accountLoginInput.Author}");

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Login account",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[AccountService.LoginAccount]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<ClaimsIdentity>> RegisterAccount(AccountInput accountInput)
    {
        try
        {
            _logger.LogInformation($"Request for register an account - {accountInput.Author}");

            var errors = await _validator.ValidateAsync(accountInput);

            if (errors.Errors.Count is not 0)
            {
                throw new AggregateException($"{errors.Errors}");
            }
            
            var account = await _unitOfWork.AccountRepository.GetByTeamNameAndAuthor(accountInput.TeamName, accountInput.Author);
            
            if (account is not null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Account with the same name already taken",
                };
            }

            account = accountInput;
            account.Password = HashPasswordHelper.HashPassowrd(account.Password);

            await _unitOfWork.AccountRepository.CreateAccount(account);

            var result = Authenticate(account);

            _logger.LogInformation($"Account created - {accountInput.Author}");

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "Account created",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[AccountService.RegisterAccount]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<string>> ChangePassword(string newPassword, int id)
    {
        try
        {
            _logger.LogInformation($"Request for change a password to - {newPassword}");
            var accountEntity = await _unitOfWork.AccountRepository.GetById(id);

            if (accountEntity is null)
            {
                return new BaseResponse<string>
                {
                    Description = "Account with the same id is not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            await _unitOfWork.AccountRepository.UpdateAccountPassword(accountEntity.Id, newPassword);

            _logger.LogInformation($"Password updated - {newPassword}");
            return new BaseResponse<string>
            {
                Data = newPassword,
                Description = "Password updated",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message, $"[AccountService.ChangePassword]: {exception.Message}");
            
            return new BaseResponse<string>
            {
                Description = exception.Message,
                StatusCode = StatusCode.NotFound
            };
        }
    }

    public async Task<IBaseResponse<string>> ChangeAuthor(int id, string newTeamName)
    {
        try
        {
            _logger.LogInformation($"Request for change a team name to - {newTeamName}");
            var accountEntity = await _unitOfWork.AccountRepository.GetById(id);

            if (accountEntity is null)
            {
                return new BaseResponse<string>
                {
                    Description = "Account with the same id is not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            await _unitOfWork.AccountRepository.UpdateAccountTeamName(accountEntity.Id, newTeamName);

            _logger.LogInformation($"Password updated - {newTeamName}");
            return new BaseResponse<string>
            {
                Data = newTeamName,
                Description = "Password updated",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message, $"[AccountService.ChangePassword]: {exception.Message}");
            
            return new BaseResponse<string>
            {
                Description = exception.Message,
                StatusCode = StatusCode.NotFound
            };
        }
    }
    
    private ClaimsIdentity Authenticate(AccountEntity account)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, account.Author),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
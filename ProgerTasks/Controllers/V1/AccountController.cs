using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProgerTasks.Domain.DTO.Account;
using ProgerTasks.Domain.Response;
using ProgerTasks.Service.Interfaces.Account;

namespace ProgerTasks.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "v1")]
public class AccountController : Controller
{
    #region initialization
    
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    #endregion

    #region standartActions
    
    /// <summary>
    /// Register account
    /// </summary>
    /// <param name="accountInput"></param>
    /// <returns>Base information about register an account</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return claims identity</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPost("register")]
    public async Task<IBaseResponse<ClaimsIdentity>> Register(AccountInput accountInput)
    {
        var response = await _accountService.RegisterAccount(accountInput);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));
            
            return response;
        }
        throw new AggregateException(nameof(response));
    }
    
    /// <summary>
    /// Login account
    /// </summary>
    /// <param name="accountLoginInput"></param>
    /// <returns>Base information about login an account</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return claims identity</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IBaseResponse<ClaimsIdentity>> Login(AccountLoginInput accountLoginInput)
    {
        var response = await _accountService.LoginAccount(accountLoginInput);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));
            
            return response;
        }
        throw new AggregateException(nameof(response));
    }
    
    /// <summary>
    /// Logout
    /// </summary>
    /// <returns>Base information about logout from account</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Logout</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
    
    #endregion

    #region changeActions
    
    /// <summary>
    /// Change Password
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newPassword"></param>
    /// <returns>Base information about change password</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return new password</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPatch("change password")]
    public async Task<IBaseResponse<string>> ChangePassword(string newPassword, int id)
    {
        var response = await _accountService.ChangePassword(newPassword, id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return response;

        throw new AggregateException(nameof(response));
    }
    
    /// <summary>
    /// Change team name
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newTeamName"></param>
    /// <returns>Base information about change team name</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return updated team name</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPatch("change team name")]
    public async Task<IBaseResponse<string>> ChangeTeamName(int id, string newTeamName)
    {
        var response = await _accountService.ChangeAuthor(id, newTeamName);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            return response;

        throw new AggregateException(nameof(response));
    }
    
    #endregion
}
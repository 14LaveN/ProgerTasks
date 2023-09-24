using Microsoft.AspNetCore.Authentication;

namespace ProgerTasks.Middlewares;

public class AuthenticationMiddleware
{
    private readonly IAuthenticationSchemeProvider _schemeProvider;

    public AuthenticationMiddleware(IAuthenticationSchemeProvider schemeProvider)
    {
        _schemeProvider = schemeProvider;
    }

    public async Task InvokeAsync(HttpContext context, Func<HttpContext, Task> next)
    {
        // Проверка подлинности
        var authenticationScheme = await _schemeProvider.GetSchemeAsync(context);
        if (authenticationScheme == null)
        {
            // Обработать ошибку
            return;
        }

        var isAuthenticated = await authenticationScheme.AuthenticateAsync(context);

        // Перенаправление на страницу входа
        if (!isAuthenticated)
        {
            context.Response.Redirect("/login", false);
            return;
        }

        // Логирование
        // ...

        await next(context);
    }
}
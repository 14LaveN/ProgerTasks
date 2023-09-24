namespace ProgerTasks.Middlewares;

public class BotProtectionMiddleware
{
    public BotProtectionMiddleware(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InvokeAsync(HttpContext context, Func<HttpContext, Task> next)
    {
        // Проверить, является ли запрос от бота
        if (IsBot(context))
        {
            // Заблокировать запрос
            context.Response.StatusCode = 403;
            return;
        }

        await next(context);
    }

    private bool IsBot(HttpContext context)
    {
        return false;
    }

    private readonly IHttpContextAccessor _httpContextAccessor;
}
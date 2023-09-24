namespace ProgerTasks.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly ILogger _logger;
    
    public RequestLoggingMiddleware(ILogger logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, Func<HttpContext, Task> next)
    {
        // Записать запрос в журнал
        _logger.LogInformation("Get request: {Request}", context.Request);

        await next(context);
    }
}
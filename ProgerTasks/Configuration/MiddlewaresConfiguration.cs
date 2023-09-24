using ProgerTasks.Middlewares;

namespace ProgerTasks.Configuration;

public static class MiddlewaresConfiguration
{
    public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
    {
        //app.UseMiddleware<ExceptionHandlingMiddleware>();
        //app.UseMiddleware<BotProtectionMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();
        return app;
    }
}
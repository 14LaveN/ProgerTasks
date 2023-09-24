using Arebis.Caching;
using DotNext.Runtime.Caching;
using ProgerTasks.Domain.Entity;

namespace ProgerTasks.Middlewares;

public class CachingMiddleware
{
    private readonly ICache<> _cache;

    public CachingMiddleware(ICache cache)
    {
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context, Func<HttpContext, Task> next)
    {
        // Получить ключ кэша
        var cacheKey = GenerateCacheKey(context);

        // Получить ответ из кэша
        var cachedResponse = _cache.Get(cacheKey);

        // Если ответ найден в кэше, вернуть его
        if (cachedResponse != null)
        {
            context.Response.StatusCode = cachedResponse.StatusCode;
            context.Response.Headers.Add(cachedResponse.Headers);
            context.Response.Body = cachedResponse.Body;
            return;
        }

        // Если ответ не найден в кэше, обработать запрос
        var response = await next(context);

        // Сохранить ответ в кэше
        _cache.Set(cacheKey, response, TimeSpan.FromMinutes(1));
    }

    private string GenerateCacheKey(HttpContext context)
    {
        // Формула для генерации ключа кэша
        return $"{context.Request.Path}{context.Request.QueryString}";
    }

    // Параметры конфигурации

    public TimeSpan CacheTimeToLive { get; set; } = TimeSpan.FromMinutes(1);
    public CacheEvictionPolicy CacheEvictionPolicy { get; set; } = CacheEvictionPolicy.LRU;
    public string[] CacheableRoutes { get; set; } = new string[] { "/api/products" };
}
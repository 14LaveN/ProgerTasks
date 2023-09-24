using Arebis.Extensions;
using CryptoExchange.Net.Interfaces;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace ProgerTasks.Middlewares;

//! public class RateLimitMiddleware
//! {
//!     private readonly IHttpContextAccessor _httpContextAccessor;
//!     private readonly ILogger<RateLimitMiddleware> _logger;
//!     private readonly IRateLimiter _rateLimiter;
//! 
//!     public RateLimitMiddleware(
//!         IHttpContextAccessor httpContextAccessor,
//!         ILogger<RateLimitMiddleware> logger,
//!         IRateLimiter rateLimiter)
//!     {
//!         _httpContextAccessor = httpContextAccessor;
//!         _logger = logger;
//!         _rateLimiter = rateLimiter;
//!     }
//! 
//!     public async Task InvokeAsync(HttpContext context, Func<HttpContext, Task> next)
//!     {
//!         // Отслеживание запроса
//!         _logger.LogInformation("Запрос получен из {IP}", context.Connection.RemoteIpAddress);
//! 
//!         // Проверка запроса на наличие признаков атаки
//!         var isAttack = await CheckForAttackAsync(context);
//! 
//!         // Блокировка запроса, если он является атакой
//!         if (isAttack)
//!         {
//!             _logger.LogWarning("Запрос отклонен из-за атаки");
//!             context.Response.StatusCode = 429;
//!             return;
//!         }
//! 
//!         // Продолжение обработки запроса
//!         await next(context);
//!     }
//! 
//!     private async Task<bool> CheckForAttackAsync(HttpContext context)
//!     {
//!         // Проверка количества запросов от данного IP-адреса за определенный период времени
//!         var rateLimit = await _rateLimiter.Try(context.Connection.RemoteIpAddress);
//! 
//!         // Если количество запросов превышает лимит, то это может быть атака
//!         return rateLimit.Requests > rateLimit.Limit;
//!     }
//! }
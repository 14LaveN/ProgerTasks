using Microsoft.AspNetCore.Authentication.Cookies;
using NLog.Web;
using ProgerTasks.Configuration;
using ProgerTasks.DAL;
using ProgerTasks.Domain;
using ProgerTasks.Service;
using ProgerTasks.SignalR;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL");

// Add services to the container.

builder.Services.AddTransient<TaskHub>();

builder.Services.AddControllers();

builder.Services.AddTransient<AppDbContext>(provier => new AppDbContext());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/User/LoginForm");
        options.AccessDeniedPath = new PathString("/User/LoginForm");
    });

builder.Services.AddCors(options => options.AddDefaultPolicy(corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins("https://localhost:44402").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddSignalR();

builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddNLogWeb("nlog.config");

builder.Host.UseNLog();

if (connectionString != null)
    builder.Services.AddDatabase(connectionString)
        .AddServices()
        .AddValidators()
        .AddSwachbackleService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

    //app.UseCustomMiddlewares();

app.MapControllers();

app.Run();
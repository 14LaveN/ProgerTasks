using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ProgerTasks.DAL;

public class AppDbContext
{
    private readonly string? _connectionString;

    public AppDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PostgreSQL");
    }
    
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
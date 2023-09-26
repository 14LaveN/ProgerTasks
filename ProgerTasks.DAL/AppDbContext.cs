using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ProgerTasks.DAL;

public class AppDbContext
{
    private readonly string? _connectionString = "Server=localhost;Port=5433;Database=ProgerTasksDevelopment;User Id=postgres;Password=1111;";

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
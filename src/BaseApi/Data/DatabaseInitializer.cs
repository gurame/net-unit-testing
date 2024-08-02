using BaseApi.Mappers;
using BaseApi.Models;
using Dapper;

namespace BaseApi.Data;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {   
        SqlMapper.AddTypeHandler(typeof(Guid), new SqLiteGuidTypeHandler());
        SqlMapper.RemoveTypeMap(typeof(Guid));
        SqlMapper.RemoveTypeMap(typeof(Guid?));

        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS Users (
            UserId TEXT PRIMARY KEY,
            FullName TEXT NOT NULL)"
            );

        var user = await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE FullName = @FullName",
            new { FullName = "Gustavo Rabanal" }
        );

        if (user == null)
        {
            await connection.ExecuteAsync(
                "INSERT INTO Users (UserId, FullName) VALUES (@UserId, @FullName)",
                new User { UserId = Guid.NewGuid(), FullName = "Gustavo Rabanal" }
            );
        }
    }
}


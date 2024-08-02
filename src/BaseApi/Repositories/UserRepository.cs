using BaseApi.Data;
using BaseApi.Models;
using Dapper;

namespace BaseApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public UserRepository(IDbConnectionFactory dbConnectionFactory)
	{
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var connection = await _dbConnectionFactory.CreateConnectionAsync();
		var users = await connection.QueryAsync<User>("SELECT * FROM Users");
		return users.ToArray();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Users WHERE UserId = @UserId";
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { UserId = id });
        return user;
    }

    public async Task<bool> CreateAsync(User user)
    {
        const string query = "INSERT INTO Users (UserId, FullName) VALUES (@UserId, @FullName)";
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteAsync(query, user) > 0;
    }
    public async  Task<bool> UpdateAsync(Guid id, User user)
    {
        const string query = "UPDATE Users SET FullName = @FullName WHERE UserId = @UserId";
        user.UserId = id;
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteAsync(query, user) > 0;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        const string query = "DELETE FROM Users WHERE UserId = @UserId";
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteAsync(query, new { UserId = id }) > 0;
    }
}

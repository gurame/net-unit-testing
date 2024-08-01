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
}

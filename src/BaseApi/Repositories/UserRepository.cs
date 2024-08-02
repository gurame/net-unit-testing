﻿using BaseApi.Data;
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
        var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var user = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = id });
        return user;
    }

    public async Task<bool> CreateAsync(User user)
    {
        var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteAsync("INSERT INTO Users (UserId, FullName) VALUES (@UserId, @FullName)", user) > 0;
    }
    public async  Task<bool> UpdateAsync(Guid id, User user)
    {
        user.UserId = id;
        var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteAsync("UPDATE Users SET FullName = @FullName WHERE UserId = @UserId", user) > 0;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteAsync("DELETE FROM Users WHERE UserId = @UserId", new { UserId = id }) > 0;
    }
}
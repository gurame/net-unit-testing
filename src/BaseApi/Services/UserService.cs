using Ardalis.Result;
using BaseApi.Logging;
using BaseApi.Models;
using BaseApi.Repositories;

namespace BaseApi.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
    private readonly ILoggerAdapter<UserService> _logger;
    public UserService(IUserRepository userRepository, ILoggerAdapter<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<User>>> GetAllAsync()
    {
        _logger.LogInformation("Getting all users");
        var users = await _userRepository.GetAllAsync();
        return users.ToList();
    }

    public async Task<Result<User>> GetByIdAsync(Guid userId)
    {
        _logger.LogInformation("Getting user by id: {UserId}", userId);
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Result.NotFound();
        }
        return user;
    }
    
    public async Task<Result<User>> CreateAsync(User user)
    {
        _logger.LogInformation("Creating user: {User}", user);
        var result = await _userRepository.CreateAsync(user);
        if (!result)
        {
            return Result.Error("Failed to create user");
        }
        var userCreated = await GetUserById(user.UserId);
        return userCreated!;
    }

    public async Task<Result<User>> UpdateAsync(Guid id, User user)
    {
        _logger.LogInformation("Updating user by id: {UserId}", id);
        var existingUser = await GetUserById(id);
        if (existingUser == null)
        {
            return Result.NotFound();
        }

        var result = await _userRepository.UpdateAsync(id, user);
        if (!result)
        {
            return Result.Error("Failed to update user");
        }

        var userUpdated = await GetUserById(user.UserId);
        return userUpdated!;
    }

    public async Task<Result> DeleteAsync(Guid userId)
    {
        _logger.LogInformation("Deleting user by id: {UserId}", userId);
        var existingUser = await GetUserById(userId);
        if (existingUser == null)
        {
            return Result.NotFound();
        }

        var result = await _userRepository.DeleteAsync(userId);
        if (!result)
        {
            return Result.Error("Failed to delete user");
        }

        return Result.NoContent();
    }

    private async Task<User?> GetUserById(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }
}

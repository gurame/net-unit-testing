using Ardalis.Result;
using BaseApi.Models;
using BaseApi.Repositories;

namespace BaseApi.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	public UserService(IUserRepository userRepository) => _userRepository = userRepository;
    public async Task<Result<IEnumerable<User>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.ToList();
    }

    public async Task<Result<User>> GetByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return Result.NotFound();
        }
        return user;
    }
    
    public async Task<Result<User>> CreateAsync(User user)
    {
        var result = await _userRepository.CreateAsync(user);
        if (!result)
        {
            return Result.Error("Failed to create user");
        }
        var userCreated = await GetUserById(user.UserId);
        return userCreated!;
    }

    public async Task<Result> DeleteAsync(Guid userId)
    {
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

    public async Task<Result<User>> UpdateAsync(Guid id, User user)
    {
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

    private async Task<User?> GetUserById(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }
}

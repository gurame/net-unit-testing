using Ardalis.Result;
using BaseApi.Contracts;
using BaseApi.Models;

namespace BaseApi.Services;

public interface IUserService
{
	Task<Result<IEnumerable<UserResponse>>> GetAllAsync();
	Task<Result<UserResponse>> GetByIdAsync(Guid id);
	Task<Result<UserResponse>> CreateAsync(User user);
	Task<Result<UserResponse>> UpdateAsync(Guid id, User user);
	Task<Result> DeleteAsync(Guid id);
}

using Ardalis.Result;
using BaseApi.Models;

namespace BaseApi.Services;

public interface IUserService
{
	Task<Result<IEnumerable<User>>> GetAllAsync();
	Task<Result<User>> GetByIdAsync(Guid id);
	Task<Result<User>> CreateAsync(User user);
	Task<Result<User>> UpdateAsync(Guid id, User user);
	Task<Result> DeleteAsync(Guid id);
}

using BaseApi.Models;

namespace BaseApi.Repositories;

public interface IUserRepository
{
	Task<IEnumerable<User>> GetAllAsync();
	Task<User?> GetByIdAsync(Guid id);
	Task<bool> CreateAsync(User user);
	Task<bool> UpdateAsync(Guid id, User user);
	Task<bool> DeleteAsync(Guid id);
}

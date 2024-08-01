using BaseApi.Models;

namespace BaseApi.Repositories;

public interface IUserRepository
{
	Task<IEnumerable<User>> GetAllAsync();
}

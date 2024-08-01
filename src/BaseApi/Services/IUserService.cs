using BaseApi.Models;

namespace BaseApi.Services;

public interface IUserService
{
	Task<IEnumerable<User>> GetAllAsync();
}

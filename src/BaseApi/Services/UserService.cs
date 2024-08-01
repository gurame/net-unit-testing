using BaseApi.Models;
using BaseApi.Repositories;

namespace BaseApi.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	public UserService(IUserRepository userRepository) => _userRepository = userRepository;
    public Task<IEnumerable<User>> GetAllAsync()
    {
        return _userRepository.GetAllAsync();
    }
}

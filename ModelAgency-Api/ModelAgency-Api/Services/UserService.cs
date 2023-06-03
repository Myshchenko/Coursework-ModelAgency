using ModelAgency_Api.Models;
using ModelAgency_Api.Repositories;

namespace ModelAgency_Api.Services
{
    public interface IUserService
    {
        Task<User> Login(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> Login(User user)
        {
            return _userRepository.Login(user);
        }
    }
}

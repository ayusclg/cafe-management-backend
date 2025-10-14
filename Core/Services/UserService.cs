using backend_01.Core.Model;
using backend_01.Infrastructure.Repository;

namespace backend_01.Core.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepo;

        public UserService(UserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            var result = await _userRepo.CreateUser(user);
            return (result);
        }
    }
}
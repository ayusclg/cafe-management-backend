using backend_01.Core.User.Model;
using backend_01.Infrastructure.Repository;
using BCrypt.Net;
using backend_01.Presentation.Request.User.Dto;
using backend_01.Presentation.Response.User.Dto ;

namespace backend_01.Core.User.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepo;

        public UserService(UserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public async Task<UserResponse.CreateUser> CreateUser(UserRequest.CreateUser user)
        {
            string password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            var newuser = new UserModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = password,
                CreatedAt = DateTime.UtcNow,
                Role=user.Role,    
            };
            var result = await _userRepo.CreateUser(newuser);
            var res = new UserResponse.CreateUser()
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email,
                CreatedAt = result.CreatedAt,
                Role=result.Role 
            };
            return res;
        }
    }
}
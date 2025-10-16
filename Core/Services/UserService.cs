using backend_01.Core.User.Model;
using backend_01.Infrastructure.Repository;
using BCrypt.Net;
using backend_01.Presentation.Request.User.Dto;
using backend_01.Presentation.Response.User.Dto ;
using Microsoft.AspNetCore.Http.HttpResults;
using backend_01.Infrastructure.Token.Service;

namespace backend_01.Core.User.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        private readonly TokenService _token;

        public UserService(UserRepository userRepository,TokenService token)
        {
            _userRepo = userRepository;
            _token = token;
        }

        public async Task<UserResponse.CreateUser> CreateUser(UserRequest.CreateUser user)
        {
            string password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new UserModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = password,
                CreatedAt = DateTime.UtcNow,
                Role = user.Role,
            };
            var result = await _userRepo.CreateUser(newUser);
            var res = new UserResponse.CreateUser()
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email,
                CreatedAt = result.CreatedAt,
                Role = result.Role
            };
            return res;
        }
        
        public async Task<UserResponse.LoginUser> login(UserRequest.LoginUser user)
        {
            var dbUser = await _userRepo.checkEmail(user.Email);
            if(dbUser == null)
            {
                throw new Exception("No User Found");
            }
            var checkPassword = await _userRepo.checkPassword(user.Password, dbUser.Password);
            if (!checkPassword)
            {
                throw new Exception($"Incorrect Password ");
            }


            var accessToken = _token.GenerateAccessToken(dbUser.Id, dbUser.UserName, dbUser.Email);
            var loginRes = new UserResponse.LoginUser()
            {
                Id = dbUser.Id,
                UserName = dbUser.UserName,
                Email = dbUser.Email,
                role = dbUser.Role,
                AccessToken = accessToken,

            };
            return loginRes;
                
        }
    }
}
using backend_01.Core.User.Model;
using backend_01.Infrastructure.Repository;
using BCrypt.Net;
using backend_01.Presentation.Request.User.Dto;
using backend_01.Presentation.Response.User.Dto ;
using Microsoft.AspNetCore.Http.HttpResults;
using backend_01.Infrastructure.Token.Service;
using backend_01.Infrastructure.Data;
using System.Security.Cryptography;
 

namespace backend_01.Core.User.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        private readonly TokenService _token;

        private readonly ApplicationDbContext _context;

        public UserService(UserRepository userRepository,TokenService token,ApplicationDbContext context)
        {
            _userRepo = userRepository;
            _token = token;
            _context = context;
        }

        public async Task<UserResponse.GetUser> CreateUser(UserRequest.CreateUser user)
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
            var res = new UserResponse.GetUser()
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
            if (dbUser == null)
            {
                throw new Exception("No User Found");
            }
            var checkPassword = await _userRepo.checkPassword(user.Password, dbUser.Password);
            if (!checkPassword)
            {
                throw new Exception($"Incorrect Password ");
            }


            var accessToken = _token.GenerateAccessToken(dbUser.Id, dbUser.UserName, dbUser.Email,dbUser.Role.ToString());
            var refreshToken = _token.generateRefreshToken();

            dbUser.RefreshToken = refreshToken;
            _context.Users.Update(dbUser);
            await _context.SaveChangesAsync();
            var loginRes = new UserResponse.LoginUser()
            {
                Id = dbUser.Id,
                UserName = dbUser.UserName,
                Email = dbUser.Email,
                role = dbUser.Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken

            };
            return loginRes;

        }

        public async Task<UserResponse.GetUser> getMyself(int id)
        {
            var user = await _userRepo.getMyself(id);
            var res = new UserResponse.GetUser()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
            };
            return res;
        }

         public async Task<UserModel> getUserAllDetails(int id)
        {
            var requestedUser = await _userRepo.getMyself(id);
            return requestedUser; 
        }
         public async Task<string> createCashierPin(UserRequest.CreatePin pin,int Id)
        {
            var createdCashierPin = string.Empty;
            if (string.IsNullOrWhiteSpace(pin.Pin))
            {
                var bytes = new byte[4];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                    int value = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
                    var newValue = value % 9000 + 1000;
                    createdCashierPin = Convert.ToString(newValue);
                }
            }
            else
            {
                createdCashierPin = pin.Pin;
            }
            var cashierDetails = await _userRepo.getMyself(Id);
            if (cashierDetails == null)
            {
                throw new Exception("Casher Not Found");
            }
            cashierDetails.CashierPin = createdCashierPin;
            _context.Users.Update(cashierDetails);
            await _context.SaveChangesAsync();
            return createdCashierPin;
            
        }
    }
}
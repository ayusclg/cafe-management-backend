using backend_01.Core.User.Model;
using backend_01.Infrastructure.Data;
using backend_01.Presentation.Request.User.Dto;
using Microsoft.EntityFrameworkCore;

namespace backend_01.Infrastructure.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error, Message:{ex.Message}");
            }
        }

        public async Task<List<UserModel>> GetUsers()
        {
            try
            {
                var users = await _context.Users.Take(10).ToListAsync()
                ;
                return users;
            }
            catch (Exception ex)
            {

                throw new Exception($"Internal Server Error,Message:{ex.Message}");
            }
        }

        public async Task<UserModel?> checkEmail(string email)
        {
            var isUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return isUser;
        }

        public async Task<bool> checkPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public async Task<UserModel> getMyself(int id)
        {
            try
            {
                var userRes = await _context.Users.FindAsync(id);
                if (userRes == null)
                {
                    throw new Exception("User Is Null");
                }
                return userRes;
            }
            catch (Exception ex)
            {

                throw new Exception($"Repository Error In Getting User,{ex.Message}");
            }
        }
        
    }
}
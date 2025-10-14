using backend_01.Core.User.Model;
using backend_01.Infrastructure.Data;
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
                List<UserModel> users= await _context.Users.Take(10).ToListAsync()
                ;
                return users;
            }
            catch (Exception ex)
            {

                throw new Exception($"Internal Server Error,Message:{ex.Message}");
            }
        }
    }
}
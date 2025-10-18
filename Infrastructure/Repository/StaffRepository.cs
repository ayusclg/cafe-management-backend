using backend_01.Core.User.Model;
using backend_01.Infrastructure.Data;
using backend_01.Presentation.Request.Staff.Dto;
using Microsoft.EntityFrameworkCore;

namespace backend_01.Infrastructure.Repository
{
    public class StaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StaffModel> CreateStaff(StaffModel staff)
        {
            try
            {
                await _context.Staffs.AddAsync(staff);
                await _context.SaveChangesAsync();
                return staff;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error, Message:{ex.Message}");
            }
        }

        public async Task<List<StaffModel>> GetStaffs()
        {
            try
            {
                var users = await _context.Staffs.Take(10).ToListAsync()
                ;
                return users;
            }
            catch (Exception ex)
            {

                throw new Exception($"Internal Server Error,Message:{ex.Message}");
            }
        }

        public async Task<StaffModel?> checkEmail(string email)
        {
            var isStaff = await _context.Staffs.FirstOrDefaultAsync(u => u.Email == email);
            return isStaff;
        }

        public async Task<bool> checkPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public async Task<StaffModel> getMyself(int id)
        {
            try
            {
                var staffRes = await _context.Staffs.FindAsync(id);
                if (staffRes == null)
                {
                    throw new Exception("Staff Is Null");
                }
                return staffRes;
            }
            catch (Exception ex)
            {

                throw new Exception($"Repository Error In Getting Staff,{ex.Message}");
            }
        }
        
    }
}
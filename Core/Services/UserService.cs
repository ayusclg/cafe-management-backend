using backend_01.Core.User.Model;
using backend_01.Infrastructure.Repository;
using BCrypt.Net;
using backend_01.Presentation.Request.Staff.Dto;
using backend_01.Presentation.Response.Staff.Dto ;
using Microsoft.AspNetCore.Http.HttpResults;
using backend_01.Infrastructure.Token.Service;
using backend_01.Infrastructure.Data;
using System.Security.Cryptography;
 

namespace backend_01.Core.User.Service
{
    public class StaffService
    {
        private readonly StaffRepository _staffRepo;
        private readonly TokenService _token;

        private readonly ApplicationDbContext _context;

        public StaffService(StaffRepository staffRepository,TokenService token,ApplicationDbContext context)
        {
            _staffRepo = staffRepository;
            _token = token;
            _context = context;
        }

        public async Task<StaffResponse.GetStaff> CreateStaff(StaffRequest.CreateStaff staff)
        {
            string password = BCrypt.Net.BCrypt.HashPassword(staff.Password);

            var newStaff = new StaffModel
            {
                UserName = staff.UserName,
                Email = staff.Email,
                Password = password,
                CreatedAt = DateTime.UtcNow,
                Role = staff.Role,
            };
            var result = await _staffRepo.CreateStaff(newStaff);
            var res = new StaffResponse.GetStaff()
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email,
                CreatedAt = result.CreatedAt,
                Role = result.Role
            };
            return res;
        }

        public async Task<StaffResponse.LoginStaff> login(StaffRequest.LoginStaff staf)
        {
            var dbStaff = await _staffRepo.checkEmail(staf.Email);
            if (dbStaff == null)
            {
                throw new Exception("No User Found");
            }
            var checkPassword = await _staffRepo.checkPassword(staf.Password, dbStaff.Password);
            if (!checkPassword)
            {
                throw new Exception($"Incorrect Password ");
            }


            var accessToken = _token.GenerateAccessToken(dbStaff.Id, dbStaff.UserName, dbStaff.Email,dbStaff.Role.ToString());
            var refreshToken = _token.generateRefreshToken();

            dbStaff.RefreshToken = refreshToken;
            _context.Staffs.Update(dbStaff);
            await _context.SaveChangesAsync();
            var loginRes = new StaffResponse.LoginStaff()
            {
                Id = dbStaff.Id,
                UserName = dbStaff.UserName,
                Email = dbStaff.Email,
                role = dbStaff.Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken

            };
            return loginRes;

        }

        public async Task<StaffResponse.GetStaff> getMyself(int id)
        {
            var user = await _staffRepo.getMyself(id);
            var res = new StaffResponse.GetStaff()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
            };
            return res;
        }

         public async Task<StaffModel> getUserAllDetails(int id)
        {
            var requestedStaff = await _staffRepo.getMyself(id);
            return requestedStaff; 
        }
         public async Task<string> createCashierPin(StaffRequest.CreatePin pin,int Id)
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
            var cashierDetails = await _staffRepo.getMyself(Id);
            if (cashierDetails == null)
            {
                throw new Exception("Casher Not Found");
            }
            cashierDetails.CashierPin = createdCashierPin;
            _context.Staffs.Update(cashierDetails);
            await _context.SaveChangesAsync();
            return createdCashierPin;
            
        }
    }
}
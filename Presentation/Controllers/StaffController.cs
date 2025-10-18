
using System.Security.Claims;
using System.Security.Cryptography;
using backend_01.Core.User.Service;
using backend_01.Presentation.Request.Staff.Dto;
using backend_01.Presentation.Response.Staff.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend_01.Presentation.Staff.Controller
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
        public class Staff:ControllerBase
    {
        private readonly StaffService _staffService;
        public Staff( StaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] StaffRequest.CreateStaff staff)
        {
            var res = await _staffService.CreateStaff(staff);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> login([FromBody] StaffRequest.LoginStaff staff)
        {
            var res = await _staffService.login(staff);
            return Ok(res);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> myself()
        {
            var user = User.FindFirstValue("userId");
            if (user == null)
            {
                return Unauthorized("User Not Logged In");
            }
            if (!int.TryParse(user, out int userId))
            {
                return BadRequest("Invalid user ID");
            }
            var userRes = await _staffService.getMyself(userId);
            return Ok(userRes);

        }

        [Authorize(Roles ="Cashier,Admin")] 
        [HttpPost]
        public async Task<IActionResult> CreateMyPin([FromBody(EmptyBodyBehavior =EmptyBodyBehavior.Allow)] StaffRequest.CreatePin pin)
        {
            var userId = User.FindFirstValue("userId");
            if (userId == null)
            {
                return Unauthorized("Please Login");
            }
            if (!int.TryParse(userId, out int id))
            {
                return BadRequest("Invalid UserId");
            }

            var result = await _staffService.createCashierPin(pin, id);
            return Created("Pin Created Successfully",result);
        }
    }
}

using System.Security.Claims;
using backend_01.Core.User.Service;
using backend_01.Presentation.Request.User.Dto;
using backend_01.Presentation.Response.User.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend_01.Presentation.User.Controller
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
        public class UserController:ControllerBase
    {
        private readonly UserService _userService;
        public UserController( UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest.CreateUser user)
        {
            var res = await _userService.CreateUser(user);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> login([FromBody] UserRequest.LoginUser user)
        {
            var res = await _userService.login(user);
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
            var userRes = await _userService.getMyself(userId);
            return Ok(userRes);
            
        }
    }
}
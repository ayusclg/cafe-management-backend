
using System.Security.Claims;
using System.Security.Cryptography;
using backend_01.Core.User.Service;
using backend_01.Presentation.Request.User.Dto;
using backend_01.Presentation.Response.User.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        [Authorize(Roles ="Cashier,Admin")] 
        [HttpPost]
        public async Task<IActionResult> CreateMyPin([FromBody(EmptyBodyBehavior =EmptyBodyBehavior.Allow)] UserRequest.CreatePin pin)
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

            var result = await _userService.createCashierPin(pin, id);
            return Created("Pin Created Successfully",result);
        }
    }
}
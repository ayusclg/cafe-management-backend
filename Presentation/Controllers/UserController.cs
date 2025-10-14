using backend_01.Core.Model;
using backend_01.Core.Service;
using backend_01.Presentation.Request.User.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace backend_01.Presentation.Controller
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
        public async Task<IActionResult> createUser([FromBody]  UserRequest.CreateUser user)
        {
            var res = await _userService.CreateUser(user);
            return Ok(res);
        }
    }
}
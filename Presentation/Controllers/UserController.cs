using backend_01.Core.Model;
using backend_01.Core.Service;
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
        public async Task<IActionResult> createUser([FromBody]  User user)
        {
            var res = await _userService.CreateUser(user);
            return Ok(res);
        }
    }
}
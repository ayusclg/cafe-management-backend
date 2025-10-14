using backend_01.Core.Menu.Service;
using backend_01.Presentation.Request.Menu.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace backend_01.Presentation.Menu.Controller
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class MenuController:ControllerBase
    {
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }
    [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuReq menu)
        {
            var res  = await _menuService.CreateMenu(menu);
            return Ok(res);
        }
    }
}
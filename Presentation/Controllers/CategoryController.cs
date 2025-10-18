using System.Security.Claims;
using backend_01.Core.Category.Service;
using backend_01.Presentation.Request.Category.Dto;
using backend_01.Presentation.Response.Category.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_01.Presentation.Category.Controller
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _category;
        public CategoryController(CategoryService category)
        {
            _category = category;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> createCategory(CreateCategoryReq category)
        {
            var userId = User.FindFirstValue("userId");
            if (userId == null)
            {
                return Unauthorized("Please Login");
            }
            if (!int.TryParse(userId, out int id))
            {
                return BadRequest("Invalid Id Format");
            }
            var createdRes = await _category.createCategory(category, id);
            return Created("Category Created Successfully", createdRes); 
        }
    }
}
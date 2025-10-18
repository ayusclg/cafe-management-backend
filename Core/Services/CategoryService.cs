using backend_01.Core.Category.Model;
using backend_01.Infrastructure.Category.Repository;
using backend_01.Presentation.Request.Category.Dto;
using backend_01.Presentation.Response.Category.Dto;

namespace backend_01.Core.Category.Service
{
    public class CategoryService
    {
        private readonly CategoryRepository _category;
        public CategoryService(CategoryRepository category)
        {
            _category = category;
        }
        public async Task<CreateCategoryRes> createCategory(CreateCategoryReq category,int id)
        {
 
            var creatingCategory = new CategoryModel
            {
                CategoryName = category.CategoryName,
                CreatedAt = DateTime.UtcNow,
                CreatedById=id,

            };
            var createRes = await _category.createCategory(creatingCategory);
            var categoryResponse = new CreateCategoryRes
            {
                Id = createRes.Id,
                CategoryName = createRes.CategoryName,
                MenuList = createRes.MenuList,
                CreatedAt = createRes.CreatedAt,
                CreatedBy=createRes.CreatedBy,
            };
            return categoryResponse;
        }
    }
}
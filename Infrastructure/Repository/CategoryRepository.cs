using backend_01.Core.Category.Model;
using backend_01.Infrastructure.Data;

namespace backend_01.Infrastructure.Category.Repository
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryModel> createCategory(CategoryModel category)
        {
 
            try
            {
                  await _context.Category.AddAsync(category);
                  await _context.SaveChangesAsync(); 
                    return category; 
            }
            catch ( Exception)
            {

                throw new Exception("Repository Error In Creating Category");
            }

        }
    }
}
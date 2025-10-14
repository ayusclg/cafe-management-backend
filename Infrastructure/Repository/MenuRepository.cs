using backend_01.Core.Menu.Model;
using backend_01.Infrastructure.Data;

namespace backend_01.Infrastructure.Menu.Repository
{
    public class MenuRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<MenuModel> CreateMenu(MenuModel menu)
        {
           try
           {
                await _context.Menus.AddAsync(menu);
                await _context.SaveChangesAsync();
                 return menu;
           }
           catch ( Exception ex)
           {

                throw new Exception($"Internal Error In Creating Menu {ex.Message}");
           }
        }
    }
}
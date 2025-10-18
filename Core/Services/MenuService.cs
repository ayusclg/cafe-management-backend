using backend_01.Core.Menu.Model;
using backend_01.Infrastructure.Menu.Repository;
using backend_01.Presentation.Request.Menu.Dto;
using backend_01.Presentation.Response.Menu.Dto;

namespace backend_01.Core.Menu.Service
{
    public class MenuService {
        private readonly MenuRepository _menuRepo;
        public MenuService(MenuRepository menuRepository)
        {
            _menuRepo = menuRepository;
        }
        
        public async Task<CreateMenuRes> CreateMenu(CreateMenuReq menu)
        {
            var newMenu = new MenuModel
            {
                MenuName = menu.MenuName,
                Price = Convert.ToDecimal(menu.Price) ,
                Ingredients = menu.Ingredients,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            var result = await _menuRepo.CreateMenu(newMenu);
            CreateMenuRes resMenu = new CreateMenuRes
            {
                Id = result.Id,
                MenuName = result.MenuName,
                Price = Convert.ToString(result.Price),
                Ingredients = result.Ingredients,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
            };
            return resMenu;

        }
    }
}
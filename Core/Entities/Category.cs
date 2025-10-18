using backend_01.Core.Menu.Model;

namespace backend_01.Core.Category.Model{
    public class CategoryModel
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public required DateTime CreatedAt { get; set; }
        
        public ICollection<MenuModel>? MenuList { get; set; }
        
    }
}
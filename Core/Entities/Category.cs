using backend_01.Core.Menu.Model;
using backend_01.Core.User.Model;

namespace backend_01.Core.Category.Model{
    public class CategoryModel
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public required DateTime CreatedAt { get; set; }
        
        public ICollection<MenuModel>? MenuList { get; set; }
        public int CreatedById { get; set; }
        public UserModel? CreatedBy{ get; set; }
    }
}
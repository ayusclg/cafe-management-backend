using backend_01.Core.Menu.Model;
using backend_01.Core.User.Model;


namespace backend_01.Presentation.Response.Category.Dto
{
    public class CreateCategoryRes
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public required DateTime CreatedAt { get; set; }
        public ICollection<MenuModel>? MenuList { get; set; }
        public StaffModel? CreatedBy{ get; set; }
    }
}
using backend_01.Core.Category.Model;

namespace backend_01.Core.Menu.Model{
    public class MenuModel
    {
        public int Id { get; set; }
        public required string MenuName { get; set; }
        public required decimal Price { get; set; }
        public required List<string> Ingredients { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel? Category{ get; set; } 
    }
}
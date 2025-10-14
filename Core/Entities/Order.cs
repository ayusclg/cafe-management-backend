namespace backend_01.Core.Menu.Model{
    public class MenuModel
    {
        public int Id { get; set; }
        public required string MenuName { get; set; }
        public required string Price { get; set; }
        public required List<string> Ingredients { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
    }
}
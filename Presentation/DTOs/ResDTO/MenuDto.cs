namespace backend_01.Presentation.Response.Menu.Dto{
    public class CreateMenuRes
    {
        public required int Id { get; set; }
        public required string MenuName { get; set; }
        public required string Price { get; set; }
        public required List<string> Ingredients { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }

    }
}
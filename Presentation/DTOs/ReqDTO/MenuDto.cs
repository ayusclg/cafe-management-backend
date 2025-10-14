namespace backend_01.Presentation.Request.Menu.Dto{
    public class CreateMenuReq
    {
        public required string MenuName { get; set; }
        public required string Price { get; set; }
        public required List<string> Ingredients{ get; set; }
    }
}
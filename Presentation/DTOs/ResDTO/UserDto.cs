namespace backend_01.Presentation.Response.User.Dto
{
    public class UserResponse
    {
        public class CreateUser
        {
            public required int Id { get; set; }
            public required string UserName { get; set; }
            public required string Email { get; set; }
            public required DateTime CreatedAt{ get; set; }
        }
        
    }
}
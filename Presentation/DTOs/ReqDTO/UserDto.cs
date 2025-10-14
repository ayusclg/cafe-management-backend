namespace backend_01.Presentation.Request.User.Dto{
    public class UserRequest
    {
        public class CreateUser
        {
             
            public required string UserName { get; set; }
            public required string Email { get; set; }
            public required string Password{ get; set; }
        }
    }
}
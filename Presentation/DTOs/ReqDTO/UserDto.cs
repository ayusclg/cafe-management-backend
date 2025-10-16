using System.Text.Json.Serialization;
using backend_01.Core.User.Model;

namespace backend_01.Presentation.Request.User.Dto{
    public class UserRequest
    {
        public class CreateUser
        {

            public required string UserName { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public required Roles Role { get; set; }
        }
         public class LoginUser
        {
            public required string Email { get; set; }
            public required string Password{ get; set; }
        }
    }
}
using System.Text.Json.Serialization;
using backend_01.Core.User.Model;

namespace backend_01.Presentation.Response.Staff.Dto
{
    public class StaffResponse
    {
        public class GetStaff
        {
            public required int Id { get; set; }
            public required string UserName { get; set; }
            public required string Email { get; set; }
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public required Roles Role { get; set; }
            public required DateTime CreatedAt { get; set; }
        }
        
        public class LoginStaff{
            public required int Id { get; set; }
            public required string UserName { get; set; }
            public required string Email { get; set; }
            public required Roles role { get; set; }
            public required string AccessToken{ get; set; }
            public required string RefreshToken{ get; set; }
        }
        
    }
}
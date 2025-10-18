using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend_01.Core.User.Model;

namespace backend_01.Presentation.Request.Staff.Dto{
    public class StaffRequest
    {
        public class CreateStaff
        {

            public required string UserName { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public required Roles Role { get; set; }
        }
        public class LoginStaff
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
        
        public class CreatePin
        {
            [StringLength(4)]
             public string? Pin { get; set; }
        }
    }
}
 

namespace backend_01.Core.User.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } 
        public required Roles Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public enum Roles{
        Admin,
        Cashier,
        Waiter
    }
}


using backend_01.Core.Category.Model;

namespace backend_01.Core.User.Model
{
    public class StaffModel
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } 
        public required Roles Role { get; set; }
        public string? RefreshToken { get; set; }
        public string? CashierPin{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<CategoryModel>? CategoryCreated{ get; set; }
    }
    public enum Roles{
        Admin,
        Cashier,
        Waiter
    }
}
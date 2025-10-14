namespace backend_01.Core.Model
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }   
        public required string Email { get; set; } 
        public required string Password { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
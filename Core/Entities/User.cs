namespace backend_01.Core.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
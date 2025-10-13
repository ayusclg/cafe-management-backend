

using System.ComponentModel.DataAnnotations;
namespace backend_01.Core.Models
{
    public class User
{
    [Key]
    public int Id { get; set; }
        [Required]
        public string? Username { get; set; }

        [EmailAddress][Required]
        public string? Email{ get; set; }

}
}
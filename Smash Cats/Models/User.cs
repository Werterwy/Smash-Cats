using System.ComponentModel.DataAnnotations;

namespace Smash_Cats.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

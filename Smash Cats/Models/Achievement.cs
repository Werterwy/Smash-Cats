using System.ComponentModel.DataAnnotations;

namespace Smash_Cats.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

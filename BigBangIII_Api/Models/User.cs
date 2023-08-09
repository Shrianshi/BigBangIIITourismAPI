using System.ComponentModel.DataAnnotations;

namespace BigBangIII_Api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MinLength(5)]
        public string? UserName { get; set; }

        [EmailAddress]
        public string? UserEmail { get; set; }
        public string? Password { get; set; }

        [Required]
        public string? Status { get; set; }
        public string? Role { get; set; }
    }
}

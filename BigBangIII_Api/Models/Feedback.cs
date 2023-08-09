using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangIII_Api.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Message { get; set; }
        public int? Rating { get; set; }

        public int UserId { get; set; }

        // Navigation property to Table1 (the referenced entity)
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}

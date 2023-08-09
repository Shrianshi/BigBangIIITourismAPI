using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangIII_Api.Models
{
    public class Agent
    {
        [Key]
        public int Agent_id { get; set; }

        [MinLength(5)]
        public string? AgentName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}

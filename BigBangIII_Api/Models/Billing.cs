using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangIII_Api.Models
{
    public class Billing
    {
        [Key]
        public int Bill_id { get; set; }
        public int? Total_cost { get; set; }
        public int? Tax { get; set; }
        public int? Book_Id { get; set; }

        [ForeignKey("Book_Id")]
        public Bookings? Bookings { get; set; }
    }
}

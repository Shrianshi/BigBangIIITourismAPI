using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangIII_Api.Models
{
    public class Bookings
    {
        [Key]
        public int Book_Id { get; set; }
        public string? Booking_date { get; set; }

        [MinLength(5)]
        public string? Tname { get; set; }
        public int? P_id { get; set; }
        [ForeignKey("P_id")]
        public Packages? Packages { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangIII_Api.Models
{
    public class Packages
    {
        [Key]
        public int P_id { get; set; }
        
        public string? P_Name { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string? Desc { get; set; }
        public int? Pricing { get; set; }
        [StringLength(200, MinimumLength = 10)]
        public string? Food_Details { get; set; }
        [StringLength(200, MinimumLength = 10)]
        public string? Acc_details { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string? ImageSrc { get; set; }
    }
}

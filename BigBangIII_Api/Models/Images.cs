using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangIII_Api.Models
{
    public class Images
    {
        [Key]
        public int Image_Id { get; set; }
        public string? IName { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}

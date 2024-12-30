using System.ComponentModel.DataAnnotations;

namespace practiceApi.Models.Dto
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public string? Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupency { get; set; }
        public string? Imageurl { get; set; }
        public string? Amenity { get; set; }
    }
}

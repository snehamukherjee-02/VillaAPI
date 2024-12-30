using System.ComponentModel.DataAnnotations;

namespace practiceApi.Models.Dto
{
    public class VillaUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public string? Rate { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public int Occupency { get; set; }
        [Required]
        public string? Imageurl { get; set; }
        public string? Amenity { get; set; }
    }
}

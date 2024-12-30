using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practiceApi.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public string? Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupency { get; set; }
        public string? Imageurl { get; set; }
        public string? Amenity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

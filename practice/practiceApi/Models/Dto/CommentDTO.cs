using System.ComponentModel.DataAnnotations;

namespace practiceApi.Models.Dto
{
    public class CommentDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid VillaId { get; set; }
    }
}

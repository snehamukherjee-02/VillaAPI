using System.ComponentModel.DataAnnotations;

namespace practiceApi.Models.Dto
{
    public class CommentCreateDTO
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}

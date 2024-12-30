using System.ComponentModel.DataAnnotations;

namespace practiceApi.Models.Dto
{
    public class CommentUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Content { get; set; }
        //[Required]
        //public int VillaId { get; set; }
    }
}

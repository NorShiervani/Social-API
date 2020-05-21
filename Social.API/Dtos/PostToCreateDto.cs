using System.ComponentModel.DataAnnotations;

namespace Social.API.Dtos
{
    public class PostToCreateDto
    {
        [Required]
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
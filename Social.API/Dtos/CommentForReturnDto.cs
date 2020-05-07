using Social.API.Models;

namespace Social.API.Dtos
{
    public class CommentForReturnDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
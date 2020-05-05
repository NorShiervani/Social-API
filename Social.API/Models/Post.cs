using System.Collections.Generic;

namespace Social.API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
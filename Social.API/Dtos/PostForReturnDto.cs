using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class PostForReturnDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
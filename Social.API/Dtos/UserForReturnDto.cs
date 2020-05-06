using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class UserForReturnDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool IsSuspended { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserConversator> UserConversators { get; set; }
    }
}
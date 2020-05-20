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
        public Role Role { get; set; }
        public ICollection<LikeForReturnDto> Likes { get; set; }
        public ICollection<PostForReturnDto> Posts { get; set; }
        public ICollection<CommentForReturnDto> Comments { get; set; }
        public ICollection<UserConversatorForReturnDto> UserConversators { get; set; }
    }
}
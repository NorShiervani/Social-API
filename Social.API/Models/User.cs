using System;
using System.Collections.Generic;

namespace Social.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool IsSuspended { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserConversator> UserConversators { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Regular,
        Moderator,
        Administrator
    }
}
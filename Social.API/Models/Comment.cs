using System;

namespace Social.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
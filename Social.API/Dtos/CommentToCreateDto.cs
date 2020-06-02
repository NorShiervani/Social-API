using System;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class CommentToCreateDto
    {
        public string Text { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
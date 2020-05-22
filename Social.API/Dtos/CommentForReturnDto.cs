using System;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class CommentForReturnDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public PostForReturnDto Post { get; set; }
        public UserForReturnDto User { get; set; }
    }
}
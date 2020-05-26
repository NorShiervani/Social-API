using System;
using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class PostForReturnDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public UserForReturnDto User { get; set; }
        public ICollection<LikeForReturnDto> Likes { get; set; }
        public ICollection<CommentForReturnDto> Comments { get; set; }
    }
}
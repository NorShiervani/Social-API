using System;

namespace Social.API.Dtos
{
    public class PostForReturnLikeDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
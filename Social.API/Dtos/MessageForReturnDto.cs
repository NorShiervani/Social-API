using System;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class MessageForReturnDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public UserConversator UserConversator { get; set; }
    }
}
using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class UserConversatorForReturnDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Conversation Conversation { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
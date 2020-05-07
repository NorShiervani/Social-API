using System.Collections.Generic;

namespace Social.API.Models
{
    public class UserConversator
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Conversation Conversation { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
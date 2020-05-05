using System.Collections.Generic;

namespace Social.API.Models
{
    public class UserConversator
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
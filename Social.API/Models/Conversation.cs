using System.Collections.Generic;

namespace Social.API.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string ConversationName { get; set; }
        public ICollection<UserConversator> UserConversators { get; set; }
    }
}
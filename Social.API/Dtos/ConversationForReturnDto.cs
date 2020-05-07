using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class ConversationForReturnDto
    {
        public int Id { get; set; }
        public string ConversationName { get; set; }
        public ICollection<UserConversator> UserConversators { get; set; }
    }
}
using System.Collections.Generic;
using Social.API.Models;

namespace Social.API.Dtos
{
    public class ConversationToCreateDto
    {
           public string ConversationName { get; set; }
             public ICollection<UserConversator> UserConversators { get; set; }
    }
}
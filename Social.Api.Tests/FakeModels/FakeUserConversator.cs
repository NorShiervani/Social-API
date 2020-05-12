using System;
using Social.API.Models;

namespace Social.Api.Tests.FakeModels
{
    public class FakeUserConversator : UserConversator
    {
        public FakeUserConversator(User user, Conversation conversation) {
            Random random = new Random();

            Id = random.Next(10000, 12000);
            User = user;
            Conversation = conversation;
        }
    }
}
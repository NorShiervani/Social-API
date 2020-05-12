using System;
using Bogus;
using Social.API.Models;

namespace Social.Api.Tests.FakeModels
{
    public class FakeConversation : Conversation
    {
        public FakeConversation() {
            Random random = new Random();
            Faker fake = new Faker();
            
            Id = random.Next(10000, 12000);
            ConversationName = fake.Company.CompanyName();
        }
    }
}
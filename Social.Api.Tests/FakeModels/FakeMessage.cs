using System;
using Bogus;
using Social.API.Models;

namespace Social.Api.Tests.FakeModels
{
    public class FakeMessage : Message
    {
        public FakeMessage(UserConversator userConversator) {
            Random random = new Random();
            Faker fake = new Faker();
            
            Id = random.Next(10000, 12000);
            Text = fake.Lorem.Sentence();
            UserConversator = userConversator;
        }
    }
}
using System;
using Bogus;
using Social.API.Models;

namespace Social.Api.Tests.FakeModels
{
    public class FakePost : Post
    {
        public FakePost(User user) {
            Random random = new Random();
            Faker fake = new Faker();
            
            Id = random.Next(10000, 12000);
            Text = fake.Lorem.Sentence();
            User = user;
        }
    }
}
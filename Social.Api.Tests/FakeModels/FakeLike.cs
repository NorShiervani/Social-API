using System;
using Social.API.Models;

namespace Social.Api.Tests.FakeModels
{
    public class FakeLike : Like
    {
        public FakeLike(User user, Post post) {
            Random random = new Random();

            Id = random.Next(10000, 12000);
            User = user;
            Post = post;
        }
    }
}
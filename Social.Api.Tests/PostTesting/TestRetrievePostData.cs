using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Social.Api.Tests.FakeModels;
using Social.API.Models;
using Social.API.Services;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Social.API;
using Microsoft.Extensions.Logging;
using Moq.EntityFrameworkCore;
using AutoFixture;

namespace Social.Api.Tests.PostTesting
{
    public class TestRetrievePostData : IClassFixture<DatabaseFixture>
    {
        IMapper _mapper;
        DatabaseFixture fixture;
        private static readonly Fixture Fixture = new Fixture();
        public TestRetrievePostData(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async void GetPostById_PostExists_ReturnsPost()
        {
            // Arrange
            IList<Post> posts = GeneratePosts();
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Posts).ReturnsDbSet(posts);
            var postRepository = new PostRepository(dataContext.Object);

            // Act
            var post = await postRepository.GetPostById(1010);

            // Assert
            Assert.Equal(1010, post.Id);
        }

        private static IList<Post> GeneratePosts()
        {
            return new List<Post>
                {
                   new FakePost(new FakeUser()) {
                       Id = 1010,
                       Text = "test"
                   },
                   new FakePost(new FakeUser()),
                   new FakePost(new FakeUser()),
                   new FakePost(new FakeUser()),
                   new FakePost(new FakeUser()),
                   new FakePost(new FakeUser()),
                   new FakePost(new FakeUser()),
                   new FakePost(new FakeUser())
                };
        }
    }
}
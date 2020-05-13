using System.Threading.Tasks;
using AutoMapper;
using Moq;
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
        [Fact]
        public async void GetPostById_PostExists_ReturnsCorrectPostId()
        {
            // Arrange
            int expectedId = 1010;
            IList<Post> posts = new List<Post> {
                    new Post() {
                       Id = expectedId,
                       Text = "This should work."
                    },
                    GenerateFake.Post(),
                    GenerateFake.Post()
            };
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Posts).ReturnsDbSet(posts);
            var postRepository = new PostRepository(dataContext.Object);

            // Act
            var post = await postRepository.GetPostById(expectedId);

            // Assert
            Assert.Equal(expectedId, post.Id);
        }
    }
}
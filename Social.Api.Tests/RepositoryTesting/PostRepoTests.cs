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
using System;

namespace Social.Api.Tests
{
    public class PostRepoTests
    {   
        private readonly Mock<DataContext> _mockContext;
        private readonly PostRepository _mockRepo;

        public PostRepoTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new PostRepository(_mockContext.Object, Mock.Of<ILogger<PostRepository>>());
        }

        [Theory]
        [InlineData(23)]
        [InlineData(554)]
        [InlineData(32)]
        [InlineData(345)]
        public async void GetPostById_PostExists_ReturnsCorrectPostId(int expectedId)
        {
            // Arrange
            IList<Post> posts = new List<Post> {
                    new Post() {
                       Id = expectedId,
                       Text = $"This post should have the Id {expectedId}."
                    },
                    GenerateFake.Post(),
                    GenerateFake.Post()
            };
            _mockContext.Setup(x => x.Posts).ReturnsDbSet(posts);

            // Act
            var post = await _mockRepo.GetPostById(expectedId);

            // Assert
            Assert.Equal(expectedId, post.Id);
        }

        
        [Theory]
        [InlineData(2)]
        [InlineData(53)]
        [InlineData(59)]
        [InlineData(151)]
        public async void GetPosts_PostsAmount_ReturnsCorrectAmountOfPosts(int expectedAmountPosts)
        {
            //Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<PostRepository> logger = loggerFactory.CreateLogger<PostRepository>();
            IList<Post> posts = new List<Post>();
            for (int i = 0; i < expectedAmountPosts; i++)
            {
                posts.Add(GenerateFake.Post());
            }
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Posts).ReturnsDbSet(posts);
            var postRepository = new PostRepository(dataContext.Object, logger);

            //Act
            var postsFromRepo = await postRepository.GetPosts();

            //Assert
            Assert.Equal(expectedAmountPosts, postsFromRepo.Count());
        }
    }
}

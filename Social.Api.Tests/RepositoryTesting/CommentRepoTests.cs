using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Social.Api.Tests;
using Social.API;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.Api.Tests
{
    public class CommentRepoTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly CommentRepository _mockRepo;

        public CommentRepoTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new CommentRepository(_mockContext.Object, Mock.Of<ILogger<CommentRepository>>());
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1000)]
        [InlineData(1313)]
        public async void GetComments_CommentsAmount_ReturnsInCorrectAmountOfComments(int incorrectAmountComments)
        {
            //Arrange
            IList<Comment> comments = new List<Comment>();
            for (int i = 0; i < 2; i++)
            {
                comments.Add(GenerateFake.Comment());
            }
            _mockContext.Setup(x => x.Comments).ReturnsDbSet(comments);

            //Act
            var commentsFromRepo = await _mockRepo.GetComments();

            //Assert
            Assert.NotEqual(incorrectAmountComments, commentsFromRepo.Count());
        }

        [Theory]
        [InlineData(50)]
        public async void GetComments_CommentsAmount_ReturnsCorrectAmountOfComments(int expectedAmountComments)
        {
            //Arrange
            IList<Comment> comments = new List<Comment>();
            for (int i = 0; i < expectedAmountComments; i++)
            {
                comments.Add(GenerateFake.Comment());
            }
            _mockContext.Setup(x => x.Comments).ReturnsDbSet(comments);

            //Act
            var commentsFromRepo = await _mockRepo.GetComments();

            //Assert
            Assert.Equal(expectedAmountComments, commentsFromRepo.Count());
        }
    }
}
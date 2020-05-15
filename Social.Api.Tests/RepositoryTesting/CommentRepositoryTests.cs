using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Social.Api.Tests;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.API.Tests.RepositoryTesting
{
    public class CommentRepositoryTests
    {
        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1000)]
        [InlineData(1313)]
        public async void GetComments_CommentsAmount_ReturnsInCorrectAmountOfComments(int incorrectAmountComments)
        {
            //Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<CommentRepository> logger = loggerFactory.CreateLogger<CommentRepository>();
            IList<Comment> comments = new List<Comment>();
            for (int i = 0; i < 2; i++)
            {
                comments.Add(GenerateFake.Comment());
            }
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Comments).ReturnsDbSet(comments);
            var commentRepository = new CommentRepository(dataContext.Object, logger);

            //Act
            var commentsFromRepo = await commentRepository.GetComments();

            //Assert
            Assert.NotEqual(incorrectAmountComments, commentsFromRepo.Count());
        }

        [Theory]
        [InlineData(50)]
        [InlineData(10)]
        [InlineData(2222)]
        [InlineData(10000)]
        [InlineData(20000)]
        public async void GetComments_CommentsAmount_ReturnsCorrectAmountOfComments(int expectedAmountComments)
        {
            //Arrange
            IList<Comment> comments = new List<Comment>();
            
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<CommentRepository> logger = loggerFactory.CreateLogger<CommentRepository>();

            for (int i = 0; i < expectedAmountComments; i++)
            {
                comments.Add(GenerateFake.Comment());
            }
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Comments).ReturnsDbSet(comments);
            var commentsRepository = new CommentRepository(dataContext.Object, logger);

            //Act
            var commentsFromRepo = await commentsRepository.GetComments();

            //Assert
            Assert.Equal(expectedAmountComments, commentsFromRepo.Count());
        }
    }
}
using Moq;
using Xunit;
using Social.API;
using Social.API.Models;
using Social.API.Services;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Social.Api.Tests
{
    public class LikeRepoTest
    {
        [Theory]
        [InlineData(500)]
        [InlineData(622)]
        [InlineData(123)]
        [InlineData(100)]
        [InlineData(10001)]
        public async void GetLikesByPostId_LikesCount_ReturnsZero(int nonExistantPostId)
        {
            // Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<LikeRepository> logger = loggerFactory.CreateLogger<LikeRepository>();
            IList<Like> likes = new List<Like> {
                    GenerateFake.Like(),
                    GenerateFake.Like(),
                    GenerateFake.Like()
            };
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Likes).ReturnsDbSet(likes);
            var likeRepository = new LikeRepository(dataContext.Object, logger);

            // Act
            var like = await likeRepository.GetLikesByPostId(nonExistantPostId);

            // Assert
            Assert.Equal(0, like.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(622)]
        [InlineData(123)]
        [InlineData(100)]
        [InlineData(10001)]
        public async void GetLikeByPostId_LikesExist_ReturnsNotNull(int ExistantPostId)
        {
            // Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<LikeRepository> logger = loggerFactory.CreateLogger<LikeRepository>();
            IList<Like> likes = new List<Like> {
                    new Like(){Id = 1, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}},
                    new Like(){Id = 2, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}},
                    new Like(){Id = 3, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}},
                    new Like(){Id = 4, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}}
            };
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Likes).ReturnsDbSet(likes);
            var likeRepository = new LikeRepository(dataContext.Object, logger);

            // Act
            var likesList = await likeRepository.GetLikesByPostId(ExistantPostId);

            // Assert
            Assert.NotNull(likesList);
        }

        [Theory]
        [InlineData(50,1001)]
        [InlineData(10,2002)]
        [InlineData(30,3003)]
        [InlineData(22,4004)]
        [InlineData(55,5005)]
        public async void GetLikeByPostId_LikesAmount_ReturnsCorrectAmountOfLikes(int expectedAmountLikes, int postId)
        {
            //Arrange
            IList<Like> likes = new List<Like>();
            
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<LikeRepository> logger = loggerFactory.CreateLogger<LikeRepository>();

            for (int i = 0; i < expectedAmountLikes; i++)
            {
                likes.Add(new Like(){Id = i+1, User = GenerateFake.User(), Post = new Post(){Id = postId}});
            }
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Likes).ReturnsDbSet(likes);
            var likeRepository = new LikeRepository(dataContext.Object, logger);

            //Act
            var likesFromRepo = await likeRepository.GetLikesByPostId(postId);

            //Assert
            Assert.Equal(expectedAmountLikes, likesFromRepo.Count());
        }



    }
}

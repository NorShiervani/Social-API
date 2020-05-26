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
    public class LikeRepoTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly LikeRepository _mockRepo;

        public LikeRepoTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new LikeRepository(_mockContext.Object, Mock.Of<ILogger<LikeRepository>>());
        }

        [Theory]
        [InlineData(500)]
        [InlineData(622)]
        [InlineData(123)]
        [InlineData(100)]
        public async void GetLikesByPostId_LikesCount_ReturnsZero(int nonExistantPostId)
        {
            // Arrange
            IList<Like> likes = new List<Like> {
                    GenerateFake.Like(),
                    GenerateFake.Like(),
                    GenerateFake.Like()
            };
            _mockContext.Setup(x => x.Likes).ReturnsDbSet(likes);

            // Act
            var like = await _mockRepo.GetLikesByPostId(nonExistantPostId);

            // Assert
            Assert.Equal(0, like.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(622)]
        [InlineData(123)]
        [InlineData(100)]
        public async void GetLikeByPostId_LikesExist_ReturnsNotNull(int ExistantPostId)
        {
            // Arrange
            IList<Like> likes = new List<Like> {
                    new Like(){Id = 1, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}},
                    new Like(){Id = 2, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}},
                    new Like(){Id = 3, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}},
                    new Like(){Id = 4, User = GenerateFake.User(), Post = new Post(){Id = ExistantPostId}}
            };
            _mockContext.Setup(x => x.Likes).ReturnsDbSet(likes);

            // Act
            var likesList = await _mockRepo.GetLikesByPostId(ExistantPostId);

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
            for (int i = 0; i < expectedAmountLikes; i++)
            {
                likes.Add(new Like(){Id = i+1, User = GenerateFake.User(), Post = new Post(){Id = postId}});
            }
            _mockContext.Setup(x => x.Likes).ReturnsDbSet(likes);

            //Act
            var likesFromRepo = await _mockRepo.GetLikesByPostId(postId);

            //Assert
            Assert.Equal(expectedAmountLikes, likesFromRepo.Count());
        }

     
        [Fact]
        public async void GetLikes_LikesIsZero_ReturnsZeroLikes()
        {
            //arrange
            IList<Like> likes = new List<Like>();
            _mockContext.Setup(x => x.Likes).ReturnsDbSet(likes);
             
            //act
            var likesFromRepo = await _mockRepo.GetLikesByPostId(44);

            //assert
            Assert.Equal(0, likes.Count());
        }
     
    }
}

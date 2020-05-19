using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Social.API;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.Api.Tests
{
    public class UserRepoTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly UserRepository _mockRepo;

        public UserRepoTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new UserRepository(_mockContext.Object, Mock.Of<ILogger<UserRepository>>());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(53)]
        [InlineData(59)]
        [InlineData(151)]
        [InlineData(157)]
        public async void GetUserById_UserExists_ReturnUser(int expectedId)
        {
            //Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UserRepository> logger = loggerFactory.CreateLogger<UserRepository>();
            IList<User> users = new List<User> {
                    new User() {
                       Id = expectedId,
                       Username = "TestPerson1",
                       Firstname ="Test",
                       Lastname = "Person",
                       Email = "testperson@test.com",
                       IsSuspended = false,
                       Country = "Sweden",
                       City = "Göteborg",
                       RoleId = 1
                    },
                    GenerateFake.User(),
                    GenerateFake.User()
            };
            _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            //Act
            var user = await _mockRepo.GetUserById(expectedId);

            //Assert
            Assert.Equal(expectedId, user.Id);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(53)]
        [InlineData(59)]
        [InlineData(151)]
        [InlineData(157)]
        public async void GetUserById_UserNotExists_ReturnNull(int nonExistantId)
        {
            //Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<UserRepository> logger = loggerFactory.CreateLogger<UserRepository>();
            IList<User> users = new List<User> {
                    new User() {
                       Id = 1000,
                       Username = "TestPerson2",
                       Firstname ="Test",
                       Lastname = "Person",
                       Email = "testperson@test.com",
                       IsSuspended = false,
                       Country = "Sweden",
                       City = "Göteborg",
                       RoleId = 1
                    },
                    GenerateFake.User(),
                    GenerateFake.User()
            };
            _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            //Act
            var user = await _mockRepo.GetUserById(nonExistantId);

            //Assert
            Assert.Null(user);
        }
    }
}
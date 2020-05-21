using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Social.API;
using Social.API.Controllers;
using Social.API.Dtos;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.Api.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserController _userController;
        private readonly Mock<IUrlHelper> _mockUrlHelper;

        public UserControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _userController = new UserController(_mockRepo.Object, _mockMapper.Object, _mockUrlHelper.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOk()
        {
            // Arrange
            IList<User> users = new List<User> {
                    GenerateFake.User(),
                    GenerateFake.User(),
                    GenerateFake.User()
            };
            _mockRepo.Setup(repo => repo.GetUsers())
                .ReturnsAsync(users);

            // Act
            var response = await _userController.GetUsers();

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        [Fact]
        public async Task GetUserById_ReturnsNoContent()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetUserById(1))
                .ReturnsAsync((User)null);

            // Act
            var response = await _userController.GetUserById(1);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(response);
        }

        [Fact]
        public async Task GetUserById_ReturnsOk() 
        {
            // Arrange
            var user = GenerateFake.User();
            _mockRepo.Setup(repo => repo.GetUserById(user.Id))
                .ReturnsAsync(user);

            // Act
            var response = await _userController.GetUserById(user.Id);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        [Fact]
        public async Task GetCommentsByUserId_ReturnsNoContent() 
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetUserById(1))
                .ReturnsAsync((User)null);

            // Act
            var response = await _userController.GetCommentsByUserId(1);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(response);
        }
    }
}
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
    public class PostControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<IPostRepository> _mockRepo;
        private readonly Mock<IUrlHelper> _urlHelper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PostController _postController;
        private readonly Mock<IUrlHelper> _mockUrlHelper;
       
        public PostControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new Mock<IPostRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _postController = new PostController(_urlHelper, _mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetPosts_ReturnsOk()
        {
            // Arrange
            IList<Post> posts = new List<Post> {
                    GenerateFake.Post(),
                    GenerateFake.Post(),
                    GenerateFake.Post()
            };
            _mockRepo.Setup(repo => repo.GetPosts())
                .ReturnsAsync(posts);

            // Act
            var response = await _postController.GetPosts();

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(response);
        }

        
        [Fact]
        public async Task GetPostById_ReturnsOk()
        {
            // Arrange
            var post = GenerateFake.Post();
            _mockRepo.Setup(repo => repo.GetPostById(post.Id))
                .ReturnsAsync(post);

            // Act
            var response = await _postController.GetPostById(post.Id);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(response);
        }

        [Fact]
        public async Task CreatePost_UsingInvalidUserId_ReturnsBadRequest()
        {
            // Arrange
            PostToCreateDto post = new PostToCreateDto() {
                Text = "Test."
            };
            _mockRepo.Setup(repo => repo.GetUserById(-1));

            // Act
            var response = await _postController.CreatePost(post);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);
        }
    }
}

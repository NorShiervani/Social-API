using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social.API;
using Social.API.Controllers;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.Api.Tests.ControllerTesting
{
    public class CommentControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<ICommentRepository> _mockRepo;
        private readonly Mock<IUrlHelper> _urlHelper;
        private readonly Mock<IMapper> _mockMapper;

        private readonly CommentController _commentController;

        public CommentControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new Mock<ICommentRepository>();
            _mockMapper = new Mock<IMapper>();
            _urlHelper = new Mock<IUrlHelper>();
            _commentController = new CommentController(_mockRepo.Object, _mockMapper.Object, _urlHelper.Object);
        }

        [Fact]
        public async Task GetCommentById_ReturnsObjectResult()
        {
            // Arrange
            Comment comment = GenerateFake.Comment();
            _mockRepo.Setup(repo => repo.GetCommentByPostId(1)).ReturnsAsync(comment);

            // Act
            var response = await _commentController.GetCommentById(1);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(response);
        }
    }
}
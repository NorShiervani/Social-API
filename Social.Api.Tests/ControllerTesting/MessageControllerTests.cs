using System.Collections;
using System.Collections.Generic;
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
    public class MessageControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<IMessageRepository> _mockRepo;
        private readonly Mock<IUrlHelper> _urlHelper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MessageController _messageController;

        public MessageControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new Mock<IMessageRepository>();
            _mockMapper = new Mock<IMapper>();
            _urlHelper = new Mock<IUrlHelper>();
            _messageController = new MessageController(_mockRepo.Object, _mockMapper.Object, _urlHelper.Object);
        }

        [Fact]
        public async Task GetMessageById_ReturnsObjectResult() {
            // Arrange
            Message message = GenerateFake.Message();
            _mockRepo.Setup(repo => repo.GetMessageById(1))
                .ReturnsAsync(message);
            
            // Act
            var response = await _messageController.GetMessageById(1);

            // Assert
            Assert.IsAssignableFrom<ObjectResult>(response);
        }
    }
}
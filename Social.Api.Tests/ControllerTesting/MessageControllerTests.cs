using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social.API;
using Social.API.Controllers;
using Social.API.Services;

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
    }
}
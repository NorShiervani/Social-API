using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social.API;
using Social.API.Controllers;
using Social.API.Services;

namespace Social.Api.Tests.ControllerTesting
{
    public class LikeControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<ILikeRepository> _mockRepo;
        private readonly Mock<IUrlHelper> _urlHelper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LikeController _likesController;

        public LikeControllerTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new Mock<ILikeRepository>();
            _mockMapper = new Mock<IMapper>();
            _urlHelper = new Mock<IUrlHelper>();
            _likesController = new LikeController(_mockRepo.Object, _mockMapper.Object);
        }
    }
}
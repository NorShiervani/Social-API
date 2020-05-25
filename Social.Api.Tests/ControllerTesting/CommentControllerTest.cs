using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social.API;
using Social.API.Controllers;
using Social.API.Services;



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
    }
}
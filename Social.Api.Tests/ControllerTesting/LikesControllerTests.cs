namespace Social.Api.Tests.ControllerTesting
{
    public class LikesControllerTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly Mock<ILikeRepository> _mockRepo;
        private readonly IUrlHelper _urlHelper;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LikesController _likesController;
    }
}
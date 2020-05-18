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

namespace Social.Api.Tests.RepositoryTesting
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
    }
}
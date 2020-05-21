using System.Linq;
using System.Collections.Generic;
using Moq;
using Moq.EntityFrameworkCore;
using Social.Api.Tests;
using Social.API.Models;
using Social.API.Services;
using Xunit;
using Microsoft.Extensions.Logging;
using Social.API;

namespace Social.Api.Tests
{
    public class ConversationRepoTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly ConversationRepository _mockRepo;

        public ConversationRepoTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new ConversationRepository(_mockContext.Object, Mock.Of<ILogger<ConversationRepository>>());
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(200)]
        public async void GetConversationById_ConversationExists_ReturnsCorrectConversationId(int expectedId)
        {
            // Arrange
            IList<Conversation> conversations = new List<Conversation> {
                    new Conversation() {
                       Id = expectedId,
                       ConversationName = "This should work."
                    },
                    GenerateFake.Conversation(),
                    GenerateFake.Conversation()
            };
            _mockContext.Setup(x => x.Conversations).ReturnsDbSet(conversations);

            // Act
            var conversation = await _mockRepo.GetConversationById(expectedId);

            // Assert
            Assert.Equal(expectedId, conversation.Id);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(300)]
        [InlineData(400)]
        public async void GetConversationById_ConversationNotExists_ReturnsNull(int nonExistantId)
        {
            // Arrange
            IList<Conversation> conversations = new List<Conversation> {
                    new Conversation() {
                       Id = 1000,
                       ConversationName = "This should work."
                    },
                    GenerateFake.Conversation(),
                    GenerateFake.Conversation()
            };
            _mockContext.Setup(x => x.Conversations).ReturnsDbSet(conversations);

            // Act
            var conversation = await _mockRepo.GetConversationById(nonExistantId);

            // Assert
            Assert.Null(conversation);
        }

        [Theory]
        [InlineData(50)]
        [InlineData(10)]
        [InlineData(2222)]
        [InlineData(10000)]
        [InlineData(20000)]
        public async void GetConversations_ConversationsAmount_ReturnsCorrectAmountOfConversations(int expectedAmountConversations)
        {
            //Arrange
            IList<Conversation> conversations = new List<Conversation>();
            for (int i = 0; i < expectedAmountConversations; i++)
            {
                conversations.Add(GenerateFake.Conversation());
            }
            _mockContext.Setup(x => x.Conversations).ReturnsDbSet(conversations);

            //Act
            var conversationsFromRepo = await _mockRepo.GetConversations();

            //Assert
            Assert.Equal(expectedAmountConversations, conversationsFromRepo.Count());
        }

        [Theory]
        [InlineData(50)]
        [InlineData(10)]
        [InlineData(2222)]
        [InlineData(10000)]
        [InlineData(20000)]
        public async void GetConversations_ConversationsAmount_ReturnsInCorrectAmountOfConversations(int incorrectAmountConversations)
        {
            //Arrange
            IList<Conversation> conversations = new List<Conversation>();
            for (int i = 0; i < 2; i++)
            {
                conversations.Add(GenerateFake.Conversation());
            }
            _mockContext.Setup(x => x.Conversations).ReturnsDbSet(conversations);

            //Act
            var conversationsFromRepo = await _mockRepo.GetConversations();

            //Assert
            Assert.NotEqual(incorrectAmountConversations, conversationsFromRepo.Count());
        }
    }
}
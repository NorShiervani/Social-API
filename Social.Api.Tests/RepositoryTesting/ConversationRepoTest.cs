using System.Collections.Generic;
using Moq;
using Moq.EntityFrameworkCore;
using Social.Api.Tests;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.API.Tests.Repository
{
    public class ConversationRepoTests
    {
        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1000)]
        [InlineData(1313)]
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
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Conversations).ReturnsDbSet(conversations);
            var conversationRepository = new ConversationRepository(dataContext.Object);

            // Act
            var conversation = await conversationRepository.GetConversationById(expectedId);

            // Assert
            Assert.Equal(expectedId, conversation.Id);
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1001)]
        [InlineData(1313)]
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
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Conversations).ReturnsDbSet(conversations);
            var conversationRepository = new ConversationRepository(dataContext.Object);

            // Act
            var conversation = await conversationRepository.GetConversationById(nonExistantId);

            // Assert
            Assert.Null(conversation);
        }
    }
}
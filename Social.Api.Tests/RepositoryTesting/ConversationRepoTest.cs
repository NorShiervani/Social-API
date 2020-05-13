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
        [Fact]
        public async void GetPostById_PostExists_ReturnsCorrectPostId()
        {
            // Arrange
            int expectedId = 2020;
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

        [Fact]
        public async void GetPostById_PostNotExists_ReturnsNull()
        {
            // Arrange
            int nonExistantId = 1;
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
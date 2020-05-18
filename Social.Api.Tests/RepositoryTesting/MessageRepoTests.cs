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
    public class MessageRepoTests
    {
        private readonly Mock<DataContext> _mockContext;
        private readonly MessageRepository _mockRepo;

        public MessageRepoTests()
        {
            _mockContext = new Mock<DataContext>();
            _mockRepo = new MessageRepository(_mockContext.Object, Mock.Of<ILogger<MessageRepository>>());
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1000)]
        [InlineData(1313)]
        public async void GetMessageById_MessageExists_ReturnsCorrectMessageId(int expectedId)
        {
            // Arrange
            IList<Message> messages = new List<Message> {
                    new Message() {
                       Id = expectedId,
                       Text = "This should work."
                    },
                    GenerateFake.Message(),
                    GenerateFake.Message()
            };
            _mockContext.Setup(x => x.Messages).ReturnsDbSet(messages);

            // Act
            var message = await _mockRepo.GetMessageById(expectedId);

            // Assert
            Assert.Equal(expectedId, message.Id);
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1001)]
        [InlineData(1313)]
        public async void GetMessageById_MessageNotExists_ReturnsNull(int nonExistantId)
        {
             // Arrange
            IList<Message> messages = new List<Message> {
                    new Message() {
                       Id = 1000,
                       Text = "This should work."
                    },
                    GenerateFake.Message(),
                    GenerateFake.Message()
            };
            _mockContext.Setup(x => x.Messages).ReturnsDbSet(messages);

            // Act
            var message = await _mockRepo.GetMessageById(nonExistantId);

            // Assert
            Assert.Null(message);
        }  

        [Theory]
        [InlineData(50)]
        [InlineData(10)]
        [InlineData(2222)]
        [InlineData(1000)]
        [InlineData(2000)]
        public async void GetMessages_MessagesAmount_ReturnsCorrectAmountOfMessages(int expectedAmountMessages)
        {
            //Arrange
            IList<Message> messages = new List<Message>();
            for (int i = 0; i < expectedAmountMessages; i++)
            {
                messages.Add(GenerateFake.Message());
            }
            _mockContext.Setup(x => x.Messages).ReturnsDbSet(messages);

            //Act
            var messagesFromRepo = await _mockRepo.GetMessages();

            //Assert
            Assert.Equal(expectedAmountMessages, messagesFromRepo.Count());
        }

        [Theory]
        [InlineData(50)]
        [InlineData(10)]
        [InlineData(2222)]
        [InlineData(10000)]
        [InlineData(20000)]
        public async void GetMessages_MessagesAmount_ReturnsInCorrectAmountOfMessages(int expectedAmountMessages)
        {
            //Arrange
            IList<Message> messages = new List<Message>();
            for (int i = 0; i < 2; i++)
            {
                messages.Add(GenerateFake.Message());
            }
            _mockContext.Setup(x => x.Messages).ReturnsDbSet(messages);

            //Act
            var messagesFromRepo = await _mockRepo.GetMessages();

            //Assert
            Assert.NotEqual(expectedAmountMessages, messagesFromRepo.Count());
        }
    }
}
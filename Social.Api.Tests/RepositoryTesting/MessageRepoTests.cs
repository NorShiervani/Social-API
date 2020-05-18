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

<<<<<<< HEAD
namespace Social.Api.Tests
=======
namespace Social.API.Tests
>>>>>>> master
{
    public class MessageRepoTests
    {
        [Theory]
        [InlineData(2000)]
        [InlineData(6666)]
        [InlineData(5000)]
        [InlineData(1000)]
        [InlineData(1313)]
        public async void GetMessageById_MessageExists_ReturnsCorrectMessageId(int expectedId)
        {
            // Arrange
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<MessageRepository> logger = loggerFactory.CreateLogger<MessageRepository>();
            IList<Message> messages = new List<Message> {
                    new Message() {
                       Id = expectedId,
                       Text = "This should work."
                    },
                    GenerateFake.Message(),
                    GenerateFake.Message()
            };
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Messages).ReturnsDbSet(messages);
            var messageRepository = new MessageRepository(dataContext.Object, logger);

            // Act
            var message = await messageRepository.GetMessageById(expectedId);

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
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<MessageRepository> logger = loggerFactory.CreateLogger<MessageRepository>();
            IList<Message> messages = new List<Message> {
                    new Message() {
                       Id = 1000,
                       Text = "This should work."
                    },
                    GenerateFake.Message(),
                    GenerateFake.Message()
            };
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Messages).ReturnsDbSet(messages);
            var messageRepository = new MessageRepository(dataContext.Object, logger);

            // Act
            var message = await messageRepository.GetMessageById(nonExistantId);

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
            
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<MessageRepository> logger = loggerFactory.CreateLogger<MessageRepository>();

            for (int i = 0; i < expectedAmountMessages; i++)
            {
                messages.Add(GenerateFake.Message());
            }
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Messages).ReturnsDbSet(messages);
            var messageRepository = new MessageRepository(dataContext.Object, logger);

            //Act
            var messagesFromRepo = await messageRepository.GetMessages();

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
            
            ILoggerFactory loggerFactory = new LoggerFactory();    
            ILogger<MessageRepository> logger = loggerFactory.CreateLogger<MessageRepository>();

            for (int i = 0; i < 2; i++)
            {
                messages.Add(GenerateFake.Message());
            }
            var dataContext = new Mock<DataContext>();
            dataContext.Setup(x => x.Messages).ReturnsDbSet(messages);
            var messageRepository = new MessageRepository(dataContext.Object, logger);

            //Act
            var messagesFromRepo = await messageRepository.GetMessages();

            //Assert
            Assert.NotEqual(expectedAmountMessages, messagesFromRepo.Count());
        }
    }
}
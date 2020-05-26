using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Social.API;
using Social.API.Controllers;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.Api.Tests
{
    public class ConversationControllerTest
    {
            private readonly Mock<DataContext> _mockContext;
            private readonly Mock<IConversationRepository> _mockRepo;
            private readonly Mock<IUrlHelper> _urlHelper;
            private readonly Mock<IMapper> _mockMapper;
            private readonly ConversationController _conversationController;

             public ConversationControllerTest()
            {
                _mockContext = new Mock<DataContext>();
                _mockRepo = new Mock<IConversationRepository>();
                _mockMapper = new Mock<IMapper>();
                _urlHelper = new Mock<IUrlHelper>();
                _conversationController = new ConversationController(_mockRepo.Object, _mockMapper.Object, _urlHelper.Object);
            }

            [Fact]
            public async Task GetConversations_ReturnOk()
            {
                //Arrange
                IList<Conversation> conversations = new List<Conversation>{
                    GenerateFake.Conversation(),
                    GenerateFake.Conversation(),
                    GenerateFake.Conversation()
                };
                _mockRepo.Setup(repo=>repo.GetConversations()).ReturnsAsync(conversations);

                //Act
                var response = await _conversationController.GetConversations();

                //Assert
                Assert.IsAssignableFrom<OkObjectResult>(response);
            }




            [Fact]
            public async Task GetConversationsById_ReturnsObjectResult()
            {
                // Arrange
                var conversation= GenerateFake.Conversation();
        
                _mockRepo.Setup( repo =>  repo.GetConversationById(conversation.Id))
                .ReturnsAsync(conversation);

                // Act
                var response = await _conversationController.GetConversationById(1);

                // Assert
                Assert.IsAssignableFrom<ObjectResult>(response);

            }
    }

}
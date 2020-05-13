using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Social.Api.Tests.FakeModels;
using Social.API.Models;
using Social.API.Services;
using Xunit;
using System.Linq;

namespace Social.Api.Tests.PostTesting
{
    public class TestRetrievePostData : IClassFixture<DatabaseFixture>
    {
        IMapper _mapper;
        DatabaseFixture fixture;
        public TestRetrievePostData(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async void GetPostById_PostExists_ReturnsPost()
        {
            //Arrange
            Post fakePost = new FakePost(new FakeUser());
            int id = fakePost.Id;
            var repoMock = new Mock<IPostRepository>();
            Post returnedPost = null;

            //Act
            fixture.dataContext.Posts.Add(fakePost);
            await fixture.dataContext.SaveChangesAsync();
            returnedPost = await fixture.dataContext.Posts.FindAsync(id);
            
            //Assert
            Assert.NotNull(returnedPost);
        }
    }
}
using AutoMapper;
using Moq;
// using Social.Api.Tests.FakeModels;
using Social.API.Models;
using Social.API.Services;
using Xunit;

namespace Social.Api.Tests
{
    public class TestRetrieveLikeData : IClassFixture<DatabaseFixture>
    {
        IMapper _mapper;
        DatabaseFixture fixture;
        public TestRetrieveLikeData(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        // [Fact]
        // public async void GetLikeByPostId_LikeExists_ReturnsLike()
        // {
        //     //Arrange
        //     FakePost fakePost = new FakePost(new FakeUser());
        //     Like fakeLike = new FakeLike(new FakeUser(),fakePost);
        //     int id = fakePost.Id;
        //     var repoMock = new Mock<ILikeRepository>();
        //     Like returneLike = null;

        //     //Act
        //     fixture.dataContext.Likes.Add(fakeLike);
        //     await fixture.dataContext.SaveChangesAsync();
        //     returneLike = await fixture.dataContext.Likes.FindAsync(id);
            
        //     //Assert
        //     Assert.NotNull(returneLike);
        // }
    }
}        

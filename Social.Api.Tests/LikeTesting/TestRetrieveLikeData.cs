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
    }
}        

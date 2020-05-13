using AutoMapper;
using Social.API.Controllers;
using Social.API.Services;
using Social.API;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Social.API.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Configuration;
using System.IO;
using Social.Api.Tests.FakeModels;
using System.Linq;

namespace Social.API.Tests
{
    public class TestRecieveUserData : IClassFixture<DatabaseFixture>
{
    IMapper _mapper;
    DatabaseFixture fixture;
    public TestRecieveUserData(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void GetUsers_Users_RetunsCorrectUserCount()
    {
        var testUser = new FakeUser();
        var testUserList = new List<User>();
        testUserList.Add(testUser);

        var controller = new TestUserController(testUserList);

        var result = controller.GetAllUsers();
        Assert.Equal(1, testUserList.Count);
    }

    [Fact]
    public async void GetUserById_UserExists_ReturnUser()
    {
        var user = await fixture.dataContext.Users.Where(x => x.Id == 1).FirstOrDefaultAsync();
        

        Assert.Equal(1, user.Id);
    }

    [Fact]
    public async void RepoTest_GetUsers()
    {
        
        if(await fixture.dataContext.Users.CountAsync() < 1)
        {
            fixture.dataContext.Users.Add(new User()
                {
                    Id = 1,
                    Username = "LitteJohn2038",
                    Password = "4321234",
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "jd@example.com",
                    IsSuspended = false,
                    Country = "England",
                    City = "Brighton",
                    RoleId = 1
                }
            );
            fixture.dataContext.SaveChanges();
        }
        
        var expectedUserCount = await fixture.dataContext.Users.CountAsync();
        var repo = new UserRepository(fixture.dataContext);
        var result = await repo.GetUsers() as List<User>;

        Assert.Equal(expectedUserCount, result.Count);

    }

    [Fact]
    public async void ControllerTest_GetAFAkeUser_WhenCalled_ReturnsAnOkUser()
    {
        var repoMock = new Mock<IUserRepository>();
        IEnumerable<User> UserList = new List<User>();
        (UserList as List<User>).Add(new User(){ Id = 2, Firstname = "Sam", Lastname="Björk" });

        repoMock.Setup(x => x.GetUsers()).Returns(Task.FromResult(UserList));

        var controller = new UserController(repoMock.Object, _mapper);

        var result = await controller.GetUsers() as OkObjectResult;
        var items = Assert.IsType<List<User>>(result.Value);
        Assert.Equal((UserList as List<User>).Count, items.Count);
    }

    private List<User> GetAllUsers()
    {
        var testUsers = new List<User>();
        testUsers.Add(new User { Id = 1, Firstname = "Sam", Lastname = "Björk"});
        testUsers.Add(new User { Id = 2, Firstname = "Sam", Lastname = "Björk"});
        testUsers.Add(new User { Id = 3, Firstname = "Sam", Lastname = "Björk"});

        return testUsers;
    }
}
}

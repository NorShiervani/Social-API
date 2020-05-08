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

public class testclass
{
    DataContext _context;
    IUserRepository _repo;
    IMapper _mapper;

    // [Fact]
    // public async void GetUsers_WhenCalled_ReturnsOkResult()
    // {
    //     var result = await _repo.GetUsers();

    //     Assert.NotNull(result);
    // }


    [Fact]
    public void GetUsers_WhenCalled_ReturnsOkResult()
    {
        var testUsers = GetAllUsers();
        var controller = new TestUserController(testUsers);

        var result = controller.GetAllUsers() as List<User>;
        Assert.Equal(testUsers.Count, result.Count);
    }

    [Fact]
    public async void ControllerTest_GetAFAkeUser_WhenCalled_ReturnsAnOkUser()
    {
        var repoMock = new Mock<IUserRepository>();
        IEnumerable<User> UserList = new List<User>();
        (UserList as List<User>).Add(new User(){ Id = 2, Firstname = "Sam", Lastname="Björk" });


        repoMock.Setup(x => x.GetUsers()).Returns(Task.FromResult(UserList));
        //var x = await repoMock.Object.GetUsers();

        var controller = new UserController(repoMock.Object, _mapper);

        var result = await controller.GetUsers() as OkObjectResult;
        var items = Assert.IsType<List<User>>(result.Value);
        Assert.Equal((UserList as List<User>).Count, items.Count);
    }
    [Fact]
    public async void RepoTest_GetUsers()
    {
        IConfiguration config = new ConfigurationBuilder()
                  .AddJsonFile($"appsettings.Development.json", true, true)
                  .Build();
        var datacontext = new DataContext(config, new DbContextOptions<DataContext>());
        var repo = new UserRepository(datacontext);

        var result = await repo.GetUsers();

    }

    private List<User> GetAllUsers()
    {
        var testUsers = new List<User>();
        testUsers.Add(new User { Id = 1, Firstname = "Sam", Lastname = "Björk"});

        return testUsers;
    }
}
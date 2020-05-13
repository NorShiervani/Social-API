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
using System.Collections;
using Moq.EntityFrameworkCore;

namespace Social.API.Tests
{
    public class TestRecieveUserData
{

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
        //Arrange
        IList<User> users = GenerateUsers();
        var userContextMock = new Mock<DataContext>();
        userContextMock.Setup(e => e.Users).ReturnsDbSet(users);
        var userRepo = new UserRepository(userContextMock.Object);   

        //Act
        var theUser = await userRepo.GetUserById(1);
        
        //Assert
        Assert.Equal(1, theUser.Id);
    }

    [Fact]
    public async void GetUserById_UserNotExists_ReturnNull()
    {
        //Arrange
        IList<User> users = GenerateUsers();
        var userContextMock = new Mock<DataContext>();
        userContextMock.Setup(e => e.Users).ReturnsDbSet(users);
        var userRepo = new UserRepository(userContextMock.Object);   

        //Act
        var theUser = await userRepo.GetUserById(999999);
        
        //Assert
        Assert.Null(theUser);
    }


        public static IList<User> GenerateUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Firstname = "The",
                    Lastname = "Duderino",
                    Country = "USA"
                }
            };
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

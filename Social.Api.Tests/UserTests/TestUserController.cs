using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Social.API.Models;

namespace Social.Api.Tests
{
    public class TestUserController : ControllerBase
    {
        public List<User> _Users = new List<User>();

        public TestUserController(List<User> Users) 
        {
            this._Users = Users;
        }

        public IList<User> GetAllUsers()
        {
            return _Users;
        }
    }
}
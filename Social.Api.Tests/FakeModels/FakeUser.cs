using System;
using System.Collections.Generic;
using Bogus;
using Social.API.Models;

namespace Social.Api.Tests.FakeModels
{
    public class FakeUser : User
    {
        public FakeUser()
        {
            Random random = new Random();
            Faker fake = new Faker();

            Id = random.Next(10000, 12000);
            Firstname = fake.Name.FirstName();
            Lastname = fake.Name.LastName();
            Username = Firstname.ToLower().Replace(" ", "") + random.Next(1337);
            Password = fake.Address.City().ToLower().Replace(" ", "") + random.Next(1337);
            Email = Firstname + "." + Lastname + "@" + fake.Company.CompanyName().Replace(" ", "") +".com";
            IsSuspended = false;
            Country = fake.Address.Country();
            City = fake.Address.City();
            RoleId = 1;
        }
    }
}
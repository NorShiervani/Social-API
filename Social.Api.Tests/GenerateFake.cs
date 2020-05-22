using System;
using Bogus;
using Social.API.Models;

namespace Social.Api.Tests
{
    public class GenerateFake
    {
        public static User User() {
            Random random = new Random();
            Faker fake = new Faker();

            return new User() {
                Id = random.Next(1000, 10000),
                Firstname = fake.Name.FirstName(),
                Lastname = fake.Name.LastName(),
                Username = fake.Address.County().Replace(" ", "") + random.Next(1337),
                Password = fake.Address.City().ToLower().Replace(" ", "") + random.Next(1337),
                Email = fake.Address.City().ToLower().Replace(" ", "") + "@" + fake.Company.CompanyName().Replace(" ", "") + ".com",
                IsSuspended = false,
                Country = fake.Address.Country(),
                City = fake.Address.City(),
                DateRegistered = DateTime.Now,
                Birthdate = DateTime.Now.AddYears(-random.Next(18, 65)),
                Role = Role.Regular
            };
        }

        public static Comment Comment() {
            Random random = new Random();
            Faker fake = new Faker();
            
            return new Comment() {
                Id = random.Next(1000, 10000),
                Text = fake.Lorem.Sentence(),
                Created = DateTime.Now,
                User = User(),
                Post = Post()
            };
        }

        public static Post Post() {
            Random random = new Random();
            Faker fake = new Faker();
            
            return new Post() {
                Id = random.Next(1000, 10000),
                Text = fake.Lorem.Sentence(),
                Created = DateTime.Now,
                User = User()
            };   
        }

        public static Like Like() {
            Random random = new Random();

            return new Like() {
                Id = random.Next(1000, 10000),
                User = User(),
                Post = Post()
            };
        }

        public static Message Message() {
            Random random = new Random();
            Faker fake = new Faker();
            
            return new Message() {
                Id = random.Next(1000, 10000),
                Text = fake.Lorem.Sentence(),
                Created = DateTime.Now,
                UserConversator = UserConversator()
            };
        }

        public static UserConversator UserConversator() {
            Random random = new Random();

            return new UserConversator() {
                Id = random.Next(1000, 10000),
                User = User(),
                Conversation = Conversation()
            };
        }

        public static Conversation Conversation() {
            Random random = new Random();
            Faker fake = new Faker();
            
            return new Conversation() {
                Id = random.Next(1000, 10000),
                ConversationName = fake.Company.CompanyName()
            };
        }
    }
}
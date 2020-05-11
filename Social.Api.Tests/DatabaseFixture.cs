using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Social.API;

namespace Social.Api.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DataContext dataContext {get; set;}
        public DatabaseFixture()
        {
            File.Delete("fake.db");
            File.Copy("../../../../Social.API/fake.db", "fake.db");
             IConfiguration config = new ConfigurationBuilder()
                  .AddJsonFile($"appsettings.json", true, true)
                  .AddJsonFile($"appsettings.Development.json", true, true)
                  .Build();

            dataContext = new DataContext(config, new DbContextOptions<DataContext>());
           
        }

        public void Dispose()
        {
            File.Delete("fake.db");
            // ... clean up test data from the database ...
        }

    }

}
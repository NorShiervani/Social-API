using Social.API.Models;
using Microsoft.EntityFrameworkCore;
using Social.API.Models.Fake;

namespace Social.API
{
    public class DataContext : DbContext
    {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Fake> Fake { get; set; }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 1, Name = "Bill"});
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 2, Name = "Shaun"});
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 3, Name = "Hillary"});
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 4, Name = "Emma"});
    }

    }
}

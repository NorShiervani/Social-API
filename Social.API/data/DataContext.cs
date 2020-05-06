using Social.API.Models;
using Microsoft.EntityFrameworkCore;
using Social.API.Models.Fake;
using Microsoft.Extensions.Configuration;

namespace Social.API
{
    public class DataContext : DbContext
    {
    private readonly IConfiguration _configuration;
    public DataContext(IConfiguration _configuration, DbContextOptions<DataContext> options) : base(options) 
    {
        this._configuration = _configuration;
    }

    public DbSet<Fake> Fake { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserConversator> UserConversators { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Conversation> Conversations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SocialNetworkDb"));
        }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 1, Name = "Bill"});
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 2, Name = "Shaun"});
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 3, Name = "Hillary"});
        modelBuilder.Entity<Fake>().HasData(new Fake{Id = 4, Name = "Emma"});
    }

    }
}

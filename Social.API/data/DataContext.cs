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

    public DataContext()
    {
    }

    public virtual DbSet<Fake> Fake { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Like> Likes { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<UserConversator> UserConversators { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<Conversation> Conversations { get; set; }

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
            
            modelBuilder.Entity<Role>().HasData(new
            {
                Id = 1,
                RoleName = "User",
                Rights = 1
            });
            modelBuilder.Entity<User>().HasData(
                new
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
            modelBuilder.Entity<User>().HasData(
                new
                {
                    Id = 2,
                    Username = "BigMan55",
                    Password = "44321554",
                    Firstname = "Patrick",
                    Lastname = "Plopinopel",
                    Email = "pp@example.com",
                    IsSuspended = false,
                    Country = "USA",
                    City = "El Paso",
                    RoleId = 1
                }
            );
            modelBuilder.Entity<User>().HasData(
                new
                {
                    Id = 3,
                    Username = "CrazyMama72",
                    Password = "44515214",
                    Firstname = "Svetlana",
                    Lastname = "Orgonsk",
                    Email = "cmso@example.com",
                    IsSuspended = false,
                    Country = "Ukraine",
                    City = "Kiev",
                    RoleId = 1
                }
            );

            modelBuilder.Entity<Like>().HasData(
                new
                {
                    Id = 1,
                    PostId = 1,
                    UserId = 2
                }
            );
            modelBuilder.Entity<Like>().HasData(
                new
                {
                    Id = 2,
                    PostId = 2,
                    UserId = 1
                }
            );
            modelBuilder.Entity<Like>().HasData(
                new
                {
                    Id = 3,
                    PostId = 1,
                    UserId = 3
                }
            );

            modelBuilder.Entity<Post>().HasData(
                new
                {
                    Id = 1,
                    Text = "Hey everybody! You all good?",
                    UserId = 2
                }
            );
            modelBuilder.Entity<Post>().HasData(
                new
                {
                    Id = 2,
                    Text = "Having the most lovely",
                    UserId = 1
                }
            );
            modelBuilder.Entity<Post>().HasData(
                new
                {
                    Id = 3,
                    Text = "Russia... Is not very nice(to us)...",
                    UserId = 3
                }
            );
             modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = 1,
                    Text = "Cool yo!",
                    PostId = 3,
                    UserId = 1
                }
            );
            modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = 2,
                    Text = "Fast as fuck!",
                    PostId = 2,
                    UserId = 2
                }
            );

            modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = 3,
                    Text = "Uuugghhh.",
                    PostId = 3,
                    UserId = 3
                }
            );
            modelBuilder.Entity<Comment>().HasData(
                new
                {
                    Id = 4,
                    Text = "Haha awesome!",
                    PostId = 3,
                    UserId = 2
                }
            );

            modelBuilder.Entity<Conversation>().HasData(
                new
                {
                    Id = 1,
                    ConversationName = "The cool guys!",
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new
                {
                    Id = 1,
                    Text = "Hello friends!",
                    UserConversatorId = 1
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new
                {
                    Id = 2,
                    Text = "Hello!",
                    UserConversatorId = 2
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new
                {
                    Id = 3,
                    Text = "What up?!",
                    UserConversatorId = 1
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new
                {
                    Id = 4,
                    Text = "Doing laundry, and you?",
                    UserConversatorId = 2
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new
                {
                    Id = 5,
                    Text = "Eating breakfast, and staying chill!",
                    UserConversatorId = 1
                }
            );

            modelBuilder.Entity<UserConversator>().HasData(
                new
                {
                    Id = 1,
                    UserId = 1,
                    ConversationId = 1
                }
            );

            modelBuilder.Entity<UserConversator>().HasData(
                new
                {
                    Id = 2,
                    UserId = 2,
                    ConversationId = 1
                }
            );
            
        }
    }
}

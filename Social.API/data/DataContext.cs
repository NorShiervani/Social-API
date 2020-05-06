using Social.API.Models;
using Microsoft.EntityFrameworkCore;
using Social.API.Models.Fake;

namespace Social.API
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Fake> Fake { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserConversator> UserConversators { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fake>().HasData(new Fake{Id = 1, Name = "Bill"});
            modelBuilder.Entity<Fake>().HasData(new Fake{Id = 2, Name = "Shaun"});
            modelBuilder.Entity<Fake>().HasData(new Fake{Id = 3, Name = "Hillary"});
            modelBuilder.Entity<Fake>().HasData(new Fake{Id = 4, Name = "Emma"});

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "LitteJohn2038",
                    Password = "4321234",
                    Firstname = "John",
                    Lastname = "Doe",
                    Email = "jd@example.com",
                    IsSuspended = false,
                    Country = "England",
                    City = "Brighton"
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Username = "BigMan55",
                    Password = "44321554",
                    Firstname = "Patrick",
                    Lastname = "Plopinopel",
                    Email = "pp@example.com",
                    IsSuspended = false,
                    Country = "USA",
                    City = "El Paso"
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 3,
                    Username = "CrazyMama72",
                    Password = "44515214",
                    Firstname = "Svetlana",
                    Lastname = "Orgonsk",
                    Email = "cmso@example.com",
                    IsSuspended = false,
                    Country = "Ukraine",
                    City = "Kiev"
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 3,
                    Username = "CrazyMama72",
                    Password = "44515214",
                    Firstname = "Svetlana",
                    Lastname = "Orgonsk",
                    Email = "cmso@example.com",
                    IsSuspended = false,
                    Country = "Ukraine",
                    City = "Kiev"
                }
            );

            modelBuilder.Entity<Like>().HasData(
                new Like
                {
                    Id = 1,
                    PostId = 1,
                    UserId = 2
                }
            );
            modelBuilder.Entity<Like>().HasData(
                new Like
                {
                    Id = 2,
                    PostId = 2,
                    UserId = 1
                }
            );
            modelBuilder.Entity<Like>().HasData(
                new Like
                {
                    Id = 3,
                    PostId = 1,
                    UserId = 3
                }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Text = "Hey everybody! You all good?",
                    UserId = 2
                }
            );
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 2,
                    Text = "Having the most lovely",
                    UserId = 1
                }
            );
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 3,
                    Text = "Russia... Is not very nice(to us)...",
                    UserId = 3
                }
            );
        }
    }
}

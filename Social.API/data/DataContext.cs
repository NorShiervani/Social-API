using Social.API.Models;
using Microsoft.EntityFrameworkCore;
using Social.API.Models.Fake;

namespace Social.API
{
    public class DataContext : DbContext
    {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Fake> Fakes { get; set; }
    }
}

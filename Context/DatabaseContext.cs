using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Context
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<Sound> Sounds { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Collection> Collections { get; set; }
  }
}

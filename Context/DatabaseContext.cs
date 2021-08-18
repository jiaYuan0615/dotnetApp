using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Context
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Profile> Profiles { get; set; }
  }
}
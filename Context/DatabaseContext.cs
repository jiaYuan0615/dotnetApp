using dotnetApp.Dvos.Member;
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
      // 要自己下SQL 需要建立 model 並且在下面加入這行
      // 外鍵關聯已經抽到 model 做完
      // 查詢會員收藏項目
      builder.Entity<MemberCollection>().HasNoKey();
    }

    // 要使用 ORM CRUD 前需要在這邊定義
    public DbSet<Member> Members { get; set; }

    // 查詢會員收藏項目
    public DbSet<MemberCollection> MemberCollections { get; set; }
    public DbSet<Sound> Sounds { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<GroupSinger> GroupSingers { get; set; }
    public DbSet<CollectionSound> CollectionSounds { get; set; }
    public DbSet<SingerSound> SingerSounds { get; set; }
  }
}

using dotnetApp.Dtos.Collection;
using dotnetApp.Dtos.Group;
using dotnetApp.Dtos.Member;
using dotnetApp.Dtos.Role;
using dotnetApp.Dtos.Singer;
using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
      // 宣告不被 migrations 追蹤請參考 ef core 5 更新文件
      // https://docs.microsoft.com/zh-tw/ef/core/what-is-new/ef-core-5.0/breaking-changes#toview
      // 外鍵關聯已經抽到 model 做完
      builder.Entity<MemberCollection>().ToTable(null);
      builder.Entity<GroupSinger>().ToTable(null);
      builder.Entity<CollectionSound>().ToTable(null);
      builder.Entity<SingerSound>().ToTable(null);
      builder.Entity<RolePermission>().ToTable(null);
    }

    // 要使用 ORM CRUD 前需要在這邊定義
    public DbSet<Member> Members { get; set; }

    // 查詢會員收藏項目
    public DbSet<MemberCollection> MemberCollections { get; set; }
    public DbSet<GroupSinger> GroupSingers { get; set; }
    public DbSet<CollectionSound> CollectionSounds { get; set; }
    public DbSet<SingerSound> SingerSongs { get; set; }
    public DbSet<Sound> Sounds { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Collection_Sound> Collection_Sound { get; set; }
    public DbSet<Singer_Sound> Singer_Sound { get; set; }
    public DbSet<Nlog> Nlogs { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission_Role> Permission_Role { get; set; }
    public DbSet<Member_Role> Member_Role { get; set; }
  }
}

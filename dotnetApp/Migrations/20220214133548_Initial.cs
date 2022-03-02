using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetApp.dotnetApp.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterDatabase()
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "groups",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            biography = table.Column<string>(type: "text", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            publishYear = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            avatar = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_groups", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "images",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            FileName = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Length = table.Column<int>(type: "int", nullable: false),
            ContentType = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            path = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_images", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "members",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            email = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            password = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            avatar = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            gender = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            email_verified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_members", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "nlogs",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            level = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            message = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            logger = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            exceptions = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            callsite = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            time = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_nlogs", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "permissions",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            title = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            action = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            status = table.Column<bool>(type: "tinyint(1)", nullable: false),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_permissions", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "roles",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            status = table.Column<bool>(type: "tinyint(1)", nullable: false),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_roles", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "sounds",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            lyrics = table.Column<string>(type: "longtext", nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            publishYear = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            cover = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            OST = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            isCover = table.Column<bool>(type: "tinyint(1)", nullable: false),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_sounds", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "singers",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            avatar = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            biography = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            groupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            nickname = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            gender = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            country = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_singers", x => x.id);
            table.ForeignKey(
                      name: "FK_singers_groups_groupId",
                      column: x => x.groupId,
                      principalTable: "groups",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "collections",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            memberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_collections", x => x.id);
            table.ForeignKey(
                      name: "FK_collections_members_memberId",
                      column: x => x.memberId,
                      principalTable: "members",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "member_role",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            memberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            roleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_member_role", x => x.id);
            table.ForeignKey(
                      name: "FK_member_role_members_memberId",
                      column: x => x.memberId,
                      principalTable: "members",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_member_role_roles_roleId",
                      column: x => x.roleId,
                      principalTable: "roles",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "permission_role",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            permissionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            roleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_permission_role", x => x.id);
            table.ForeignKey(
                      name: "FK_permission_role_permissions_permissionId",
                      column: x => x.permissionId,
                      principalTable: "permissions",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_permission_role_roles_roleId",
                      column: x => x.roleId,
                      principalTable: "roles",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "group_sound",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            groupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            soundId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_group_sound", x => x.id);
            table.ForeignKey(
                      name: "FK_group_sound_groups_groupId",
                      column: x => x.groupId,
                      principalTable: "groups",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_group_sound_sounds_soundId",
                      column: x => x.soundId,
                      principalTable: "sounds",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "singer_sound",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            singerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            soundId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_singer_sound", x => x.id);
            table.ForeignKey(
                      name: "FK_singer_sound_singers_singerId",
                      column: x => x.singerId,
                      principalTable: "singers",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_singer_sound_sounds_soundId",
                      column: x => x.soundId,
                      principalTable: "sounds",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "collection_sound",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            collectionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            soundId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            createdAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            updatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_collection_sound", x => x.id);
            table.ForeignKey(
                      name: "FK_collection_sound_collections_collectionId",
                      column: x => x.collectionId,
                      principalTable: "collections",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_collection_sound_sounds_soundId",
                      column: x => x.soundId,
                      principalTable: "sounds",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateIndex(
          name: "IX_collection_sound_collectionId",
          table: "collection_sound",
          column: "collectionId");

      migrationBuilder.CreateIndex(
          name: "IX_collection_sound_soundId",
          table: "collection_sound",
          column: "soundId");

      migrationBuilder.CreateIndex(
          name: "IX_collections_memberId",
          table: "collections",
          column: "memberId");

      migrationBuilder.CreateIndex(
          name: "IX_group_sound_groupId",
          table: "group_sound",
          column: "groupId");

      migrationBuilder.CreateIndex(
          name: "IX_group_sound_soundId",
          table: "group_sound",
          column: "soundId");

      migrationBuilder.CreateIndex(
          name: "IX_member_role_memberId",
          table: "member_role",
          column: "memberId");

      migrationBuilder.CreateIndex(
          name: "IX_member_role_roleId",
          table: "member_role",
          column: "roleId");

      migrationBuilder.CreateIndex(
          name: "IX_permission_role_permissionId",
          table: "permission_role",
          column: "permissionId");

      migrationBuilder.CreateIndex(
          name: "IX_permission_role_roleId",
          table: "permission_role",
          column: "roleId");

      migrationBuilder.CreateIndex(
          name: "IX_singer_sound_singerId",
          table: "singer_sound",
          column: "singerId");

      migrationBuilder.CreateIndex(
          name: "IX_singer_sound_soundId",
          table: "singer_sound",
          column: "soundId");

      migrationBuilder.CreateIndex(
          name: "IX_singers_groupId",
          table: "singers",
          column: "groupId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "collection_sound");

      migrationBuilder.DropTable(
          name: "group_sound");

      migrationBuilder.DropTable(
          name: "images");

      migrationBuilder.DropTable(
          name: "member_role");

      migrationBuilder.DropTable(
          name: "nlogs");

      migrationBuilder.DropTable(
          name: "permission_role");

      migrationBuilder.DropTable(
          name: "singer_sound");

      migrationBuilder.DropTable(
          name: "collections");

      migrationBuilder.DropTable(
          name: "permissions");

      migrationBuilder.DropTable(
          name: "roles");

      migrationBuilder.DropTable(
          name: "singers");

      migrationBuilder.DropTable(
          name: "sounds");

      migrationBuilder.DropTable(
          name: "members");

      migrationBuilder.DropTable(
          name: "groups");
    }
  }
}

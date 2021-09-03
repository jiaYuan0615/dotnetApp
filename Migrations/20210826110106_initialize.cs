using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetApp.Migrations
{
  public partial class initialize : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterDatabase()
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Collections",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            memberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Collections", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Groups",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Groups", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Members",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            email = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            password = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            avatar = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            gender = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            email_verified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Members", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Singers",
          columns: table => new
          {
            id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            avatar = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            biography = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            groupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            nickname = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            gender = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            country = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Singers", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Sounds",
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
            isCover = table.Column<bool>(type: "tinyint(1)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Sounds", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Collections");

      migrationBuilder.DropTable(
          name: "Groups");

      migrationBuilder.DropTable(
          name: "Members");

      migrationBuilder.DropTable(
          name: "Singers");

      migrationBuilder.DropTable(
          name: "Sounds");
    }
  }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using dotnetApp.dotnetApp.Context;

namespace dotnetApp.dotnetApp.Migrations
{
  [DbContext(typeof(DatabaseContext))]
  partial class DatabaseContextModelSnapshot : ModelSnapshot
  {
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("Relational:MaxIdentifierLength", 64)
          .HasAnnotation("ProductVersion", "5.0.9");

      modelBuilder.Entity("dotnetApp.Dtos.Collection.CollectionSound", b =>
          {
            b.Property<string>("id")
                      .HasColumnType("varchar(255)");

            b.Property<string>("createdAt")
                      .HasColumnType("longtext");

            b.Property<string>("name")
                      .HasColumnType("longtext");

            b.Property<string>("soundId")
                      .HasColumnType("longtext");

            b.Property<string>("soundName")
                      .HasColumnType("longtext");

            b.Property<string>("soundPublishYear")
                      .HasColumnType("longtext");

            b.HasKey("id");

            b.ToTable(null);
          });

      modelBuilder.Entity("dotnetApp.Dtos.Group.GroupSinger", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("name")
                      .HasColumnType("longtext");

            b.Property<string>("singerAvatar")
                      .HasColumnType("longtext");

            b.Property<string>("singerBirth")
                      .HasColumnType("longtext");

            b.Property<string>("singerCountry")
                      .HasColumnType("longtext");

            b.Property<string>("singerGender")
                      .HasColumnType("longtext");

            b.Property<string>("singerId")
                      .HasColumnType("longtext");

            b.Property<string>("singerName")
                      .HasColumnType("longtext");

            b.Property<string>("singerNickname")
                      .HasColumnType("longtext");

            b.HasKey("id");

            b.ToTable(null);
          });

      modelBuilder.Entity("dotnetApp.Dtos.Member.MemberCollection", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<Guid>("collectionId")
                      .HasColumnType("char(36)");

            b.Property<string>("collectionName")
                      .HasColumnType("longtext");

            b.Property<string>("email")
                      .HasColumnType("longtext");

            b.Property<string>("gender")
                      .HasColumnType("longtext");

            b.Property<string>("name")
                      .HasColumnType("longtext");

            b.HasKey("id");

            b.ToTable(null);
          });

      modelBuilder.Entity("dotnetApp.Dtos.Role.RolePermission", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("action")
                      .HasColumnType("longtext");

            b.Property<string>("name")
                      .HasColumnType("longtext");

            b.Property<Guid>("permissionId")
                      .HasColumnType("char(36)");

            b.Property<bool>("status")
                      .HasColumnType("tinyint(1)");

            b.HasKey("id");

            b.ToTable(null);
          });

      modelBuilder.Entity("dotnetApp.Dtos.Singer.SingerGroup", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("avatar")
                      .HasColumnType("longtext");

            b.Property<DateTime>("birth")
                      .HasColumnType("datetime(6)");

            b.Property<string>("country")
                      .HasColumnType("longtext");

            b.Property<string>("gender")
                      .HasColumnType("longtext");

            b.Property<DateTime>("groupCreatedAt")
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("groupId")
                      .HasColumnType("char(36)");

            b.Property<string>("groupName")
                      .HasColumnType("longtext");

            b.Property<DateTime>("groupupdatedAt")
                      .HasColumnType("datetime(6)");

            b.Property<string>("name")
                      .HasColumnType("longtext");

            b.Property<string>("nickname")
                      .HasColumnType("longtext");

            b.HasKey("id");

            b.ToTable(null);
          });

      modelBuilder.Entity("dotnetApp.Dtos.Singer.SingerSound", b =>
          {
            b.Property<string>("id")
                      .HasColumnType("varchar(255)");

            b.Property<string>("avatar")
                      .HasColumnType("longtext");

            b.Property<string>("birth")
                      .HasColumnType("longtext");

            b.Property<string>("country")
                      .HasColumnType("longtext");

            b.Property<string>("gender")
                      .HasColumnType("longtext");

            b.Property<string>("groupId")
                      .HasColumnType("longtext");

            b.Property<string>("groupName")
                      .HasColumnType("longtext");

            b.Property<string>("name")
                      .HasColumnType("longtext");

            b.Property<string>("nickName")
                      .HasColumnType("longtext");

            b.Property<string>("soundId")
                      .HasColumnType("longtext");

            b.Property<string>("soundName")
                      .HasColumnType("longtext");

            b.Property<string>("soundPublishYear")
                      .HasColumnType("longtext");

            b.HasKey("id");

            b.ToTable(null);
          });

      modelBuilder.Entity("dotnetApp.Models.Collection", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("memberId")
                      .HasColumnType("char(36)");

            b.Property<string>("name")
                      .IsRequired()
                      .HasMaxLength(30)
                      .HasColumnType("varchar(30)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("memberId");

            b.ToTable("collections");
          });

      modelBuilder.Entity("dotnetApp.Models.Collection_Sound", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<Guid>("collectionId")
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("soundId")
                      .HasColumnType("char(36)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("collectionId");

            b.HasIndex("soundId");

            b.ToTable("collection_sound");
          });

      modelBuilder.Entity("dotnetApp.Models.Group", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("avatar")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<string>("biography")
                      .HasColumnType("text");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<string>("name")
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnType("varchar(50)");

            b.Property<string>("publishYear")
                      .HasMaxLength(7)
                      .HasColumnType("varchar(7)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.ToTable("groups");
          });

      modelBuilder.Entity("dotnetApp.Models.Group_Sound", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("groupId")
                      .HasColumnType("char(36)");

            b.Property<Guid>("soundId")
                      .HasColumnType("char(36)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("groupId");

            b.HasIndex("soundId");

            b.ToTable("group_sound");
          });

      modelBuilder.Entity("dotnetApp.Models.Image", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("ContentType")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<string>("FileName")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<int>("Length")
                      .HasColumnType("int");

            b.Property<string>("path")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.HasKey("id");

            b.ToTable("images");
          });

      modelBuilder.Entity("dotnetApp.Models.Member", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("avatar")
                      .HasColumnType("longtext");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<string>("email")
                      .IsRequired()
                      .HasMaxLength(60)
                      .HasColumnType("varchar(60)");

            b.Property<DateTime?>("email_verified")
                      .HasColumnType("datetime(6)");

            b.Property<string>("gender")
                      .IsRequired()
                      .HasMaxLength(1)
                      .HasColumnType("varchar(1)");

            b.Property<string>("name")
                      .IsRequired()
                      .HasMaxLength(20)
                      .HasColumnType("varchar(20)");

            b.Property<string>("password")
                      .IsRequired()
                      .HasMaxLength(60)
                      .HasColumnType("varchar(60)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.ToTable("members");
          });

      modelBuilder.Entity("dotnetApp.Models.Member_Role", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("memberId")
                      .HasColumnType("char(36)");

            b.Property<Guid>("roleId")
                      .HasColumnType("char(36)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("memberId");

            b.HasIndex("roleId");

            b.ToTable("member_role");
          });

      modelBuilder.Entity("dotnetApp.Models.Nlog", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("callsite")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<string>("exceptions")
                      .HasColumnType("longtext");

            b.Property<string>("level")
                      .IsRequired()
                      .HasMaxLength(10)
                      .HasColumnType("varchar(10)");

            b.Property<string>("logger")
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnType("varchar(255)");

            b.Property<string>("message")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<DateTime>("time")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.ToTable("nlogs");
          });

      modelBuilder.Entity("dotnetApp.Models.Permission", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("action")
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnType("varchar(100)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<bool>("status")
                      .HasColumnType("tinyint(1)");

            b.Property<string>("title")
                      .IsRequired()
                      .HasMaxLength(20)
                      .HasColumnType("varchar(20)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.ToTable("permissions");
          });

      modelBuilder.Entity("dotnetApp.Models.Permission_Role", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("permissionId")
                      .HasColumnType("char(36)");

            b.Property<Guid>("roleId")
                      .HasColumnType("char(36)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("permissionId");

            b.HasIndex("roleId");

            b.ToTable("permission_role");
          });

      modelBuilder.Entity("dotnetApp.Models.Role", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<string>("name")
                      .IsRequired()
                      .HasMaxLength(20)
                      .HasColumnType("varchar(20)");

            b.Property<bool>("status")
                      .HasColumnType("tinyint(1)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.ToTable("roles");
          });

      modelBuilder.Entity("dotnetApp.Models.Singer", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("avatar")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<string>("biography")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<DateTime>("birth")
                      .HasColumnType("datetime(6)");

            b.Property<string>("country")
                      .IsRequired()
                      .HasMaxLength(20)
                      .HasColumnType("varchar(20)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<string>("gender")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<Guid>("groupId")
                      .HasColumnType("char(36)");

            b.Property<string>("name")
                      .IsRequired()
                      .HasMaxLength(60)
                      .HasColumnType("varchar(60)");

            b.Property<string>("nickname")
                      .IsRequired()
                      .HasMaxLength(60)
                      .HasColumnType("varchar(60)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("groupId");

            b.ToTable("singers");
          });

      modelBuilder.Entity("dotnetApp.Models.Singer_Sound", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<Guid>("singerId")
                      .HasColumnType("char(36)");

            b.Property<Guid>("soundId")
                      .HasColumnType("char(36)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.HasIndex("singerId");

            b.HasIndex("soundId");

            b.ToTable("singer_sound");
          });

      modelBuilder.Entity("dotnetApp.Models.Sound", b =>
          {
            b.Property<Guid>("id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("char(36)");

            b.Property<string>("OST")
                      .IsRequired()
                      .HasMaxLength(30)
                      .HasColumnType("varchar(30)");

            b.Property<string>("cover")
                      .IsRequired()
                      .HasColumnType("longtext");

            b.Property<DateTime>("createdAt")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("datetime(6)");

            b.Property<bool>("isCover")
                      .HasColumnType("tinyint(1)");

            b.Property<string>("lyrics")
                      .HasColumnType("longtext");

            b.Property<string>("name")
                      .IsRequired()
                      .HasMaxLength(60)
                      .HasColumnType("varchar(60)");

            b.Property<string>("publishYear")
                      .IsRequired()
                      .HasMaxLength(4)
                      .HasColumnType("varchar(4)");

            b.Property<DateTime>("updatedAt")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasColumnType("datetime(6)");

            b.HasKey("id");

            b.ToTable("sounds");
          });

      modelBuilder.Entity("dotnetApp.Models.Collection", b =>
          {
            b.HasOne("dotnetApp.Models.Member", "Member")
                      .WithMany("Collections")
                      .HasForeignKey("memberId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Member");
          });

      modelBuilder.Entity("dotnetApp.Models.Collection_Sound", b =>
          {
            b.HasOne("dotnetApp.Models.Collection", "Collection")
                      .WithMany()
                      .HasForeignKey("collectionId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("dotnetApp.Models.Sound", "Sound")
                      .WithMany()
                      .HasForeignKey("soundId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Collection");

            b.Navigation("Sound");
          });

      modelBuilder.Entity("dotnetApp.Models.Group_Sound", b =>
          {
            b.HasOne("dotnetApp.Models.Group", "Group")
                      .WithMany()
                      .HasForeignKey("groupId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("dotnetApp.Models.Sound", "Sound")
                      .WithMany()
                      .HasForeignKey("soundId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Group");

            b.Navigation("Sound");
          });

      modelBuilder.Entity("dotnetApp.Models.Member_Role", b =>
          {
            b.HasOne("dotnetApp.Models.Member", "Member")
                      .WithMany()
                      .HasForeignKey("memberId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("dotnetApp.Models.Role", "Role")
                      .WithMany()
                      .HasForeignKey("roleId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Member");

            b.Navigation("Role");
          });

      modelBuilder.Entity("dotnetApp.Models.Permission_Role", b =>
          {
            b.HasOne("dotnetApp.Models.Permission", "Permission")
                      .WithMany()
                      .HasForeignKey("permissionId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("dotnetApp.Models.Role", "Role")
                      .WithMany()
                      .HasForeignKey("roleId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Permission");

            b.Navigation("Role");
          });

      modelBuilder.Entity("dotnetApp.Models.Singer", b =>
          {
            b.HasOne("dotnetApp.Models.Group", "Group")
                      .WithMany()
                      .HasForeignKey("groupId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Group");
          });

      modelBuilder.Entity("dotnetApp.Models.Singer_Sound", b =>
          {
            b.HasOne("dotnetApp.Models.Singer", "Singer")
                      .WithMany()
                      .HasForeignKey("singerId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.HasOne("dotnetApp.Models.Sound", "Sound")
                      .WithMany()
                      .HasForeignKey("soundId")
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

            b.Navigation("Singer");

            b.Navigation("Sound");
          });

      modelBuilder.Entity("dotnetApp.Models.Member", b =>
          {
            b.Navigation("Collections");
          });
#pragma warning restore 612, 618
    }
  }
}

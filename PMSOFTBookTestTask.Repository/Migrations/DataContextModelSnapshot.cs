﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PMSOFTBookTestTask.Repository.Context;

#nullable disable

namespace PMSOFTBookTestTask.Repository.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.AuthorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d7ed2451-0f19-49dc-830e-fbe2d1c8bbcf"),
                            AuthorName = "Ben Watson"
                        },
                        new
                        {
                            Id = new Guid("df842420-d936-4030-acfa-25761db07a02"),
                            AuthorName = "Jeffrey Richter"
                        });
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.BookEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("41823574-1739-4b8f-bb76-97df3d918cfa"),
                            AuthorId = new Guid("d7ed2451-0f19-49dc-830e-fbe2d1c8bbcf"),
                            GenreId = new Guid("f93ca5c6-091e-4709-adb9-211086f8fd00"),
                            Name = "Высокопроизводительный код на платформе .NET",
                            Year = 2019
                        },
                        new
                        {
                            Id = new Guid("b101d34d-6e04-4e92-9b3e-bf97e34ac9ae"),
                            AuthorId = new Guid("df842420-d936-4030-acfa-25761db07a02"),
                            GenreId = new Guid("f93ca5c6-091e-4709-adb9-211086f8fd00"),
                            Name = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#",
                            Year = 2002
                        });
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.GenreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f93ca5c6-091e-4709-adb9-211086f8fd00"),
                            GenreName = "IT Education"
                        });
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("599fea4c-46d3-491d-a9b2-d408b017ea76"),
                            Role = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("585a13a7-62ff-4976-896c-5cf70dd3c6b7"),
                            Role = "User"
                        });
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("33bdcabc-3f00-4aa7-ae3b-d274eee520ef"),
                            Email = "admin@gmail.com",
                            Password = "$2a$11$2LjY4Q1k.dXMNY0pui8u4.tGVGpVAkSpnIhpWeJO.smZGIOPIlN2i",
                            RoleId = new Guid("599fea4c-46d3-491d-a9b2-d408b017ea76"),
                            Username = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("e983f9ea-ea3f-45ca-840e-86725e6d6963"),
                            Email = "user@gmail.com",
                            Password = "$2a$11$gWkXbP6mBx5.taFglFQfke2vpZKB43Q5vh.aiRWXJQoLWZ0a5bNuO",
                            RoleId = new Guid("585a13a7-62ff-4976-896c-5cf70dd3c6b7"),
                            Username = "User"
                        });
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.BookEntity", b =>
                {
                    b.HasOne("PMSOFTBookTestTask.Repository.Entities.AuthorEntity", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PMSOFTBookTestTask.Repository.Entities.GenreEntity", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.UserEntity", b =>
                {
                    b.HasOne("PMSOFTBookTestTask.Repository.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.AuthorEntity", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.GenreEntity", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("PMSOFTBookTestTask.Repository.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

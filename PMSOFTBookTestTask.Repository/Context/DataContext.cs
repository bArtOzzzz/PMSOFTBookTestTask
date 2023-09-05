using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMSOFTBookTestTask.Repository.Entities;

namespace PMSOFTBookTestTask.Repository.Context
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> option,
                           IConfiguration configuration) : base(option)
        {
            _configuration = configuration;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Database.EnsureCreated();
        }

        public DbSet<AuthorEntity> Authors { get; set; } = default!;
        public DbSet<GenreEntity> Genres { get; set; } = default!;
        public DbSet<BookEntity> Books { get; set; } = default!;
        public DbSet<UserEntity> Users { get; set; } = default!;
        public DbSet<RoleEntity> Roles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid[] GuidRoleArr = { Guid.NewGuid(), Guid.NewGuid() };
            Guid[] GuidAuthorArr = { Guid.NewGuid(), Guid.NewGuid() };
            Guid[] GuidGenreArr = { Guid.NewGuid() };

            modelBuilder.Entity<AuthorEntity>(
                entity =>
                {
                    entity.Property(x => x.Id)
                          .IsRequired();
                });

            modelBuilder.Entity<GenreEntity>(
                entity =>
                {
                    entity.Property(x => x.Id)
                          .IsRequired();
                });

            modelBuilder.Entity<UserEntity>(
                entity =>
                {
                    entity.Property(x => x.Id)
                          .IsRequired();

                    entity.HasOne(u => u.Role)
                          .WithMany(r => r.Users)
                          .HasForeignKey(u => u.RoleId)
                          .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<BookEntity>(
                entity =>
                {
                    entity.Property(x => x.Id)
                          .IsRequired();

                    entity.HasOne(e => e.Genre)
                          .WithMany(e => e.Books)
                          .HasForeignKey(e => e.GenreId);

                    entity.HasOne(e => e.Author)
                          .WithMany(e => e.Books)
                          .HasForeignKey(e => e.AuthorId);
                });

            modelBuilder.Entity<AuthorEntity>().HasData(
               new AuthorEntity
               {
                   Id = GuidAuthorArr[0],
                   AuthorName = "Ben Watson"
               },

               new AuthorEntity
               {
                   Id = GuidAuthorArr[1],
                   AuthorName = "Jeffrey Richter"
               });

            modelBuilder.Entity<GenreEntity>().HasData(
                new GenreEntity
                {
                    Id = GuidGenreArr[0],
                    GenreName = "IT Education"
                });

            modelBuilder.Entity<BookEntity>().HasData(
                new BookEntity
                {
                    Id = Guid.NewGuid(),
                    GenreId = GuidGenreArr[0],
                    AuthorId = GuidAuthorArr[0],
                    Name = "Высокопроизводительный код на платформе .NET",
                    Year = 2019
                },

                new BookEntity
                {
                    Id = Guid.NewGuid(),
                    GenreId = GuidGenreArr[0],
                    AuthorId = GuidAuthorArr[1],
                    Name = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#",
                    Year = 2002
                }) ;

            modelBuilder.Entity<RoleEntity>().HasData(
                 new RoleEntity
                 {
                     Id = GuidRoleArr[0],
                     Role = "Administrator"
                 },

                 new RoleEntity
                 {
                     Id = GuidRoleArr[1],
                     Role = "User"
                 });

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "Administrator",
                    Email = "admin@gmail.com",
                    RoleId = GuidRoleArr[0],
                    Password = BCrypt.Net.BCrypt.HashPassword(_configuration["Secrets:Administrator"])
                },

                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Username = "User",
                    Email = "user@gmail.com",
                    RoleId = GuidRoleArr[1],
                    Password = BCrypt.Net.BCrypt.HashPassword(_configuration["Secrets:User"])
                });
        }
    }
}

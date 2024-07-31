using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Infrastructure
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<UserEntity> users = new ()
            {
                new UserEntity
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    Email = "john@gmail.com",
                    Password = "123456"
                },
                new UserEntity
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Doe",
                    Email = "jane@gmail.com",
                    Password = "123456"
                },
                new UserEntity
                {
                    Id = 3,
                    Name = "Alice",
                    Surname = "Doe",
                    Email = "alice@gmail.com",
                    Password = "123456"
                }
            };

            modelBuilder.Entity<UserEntity>().HasData(users);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}

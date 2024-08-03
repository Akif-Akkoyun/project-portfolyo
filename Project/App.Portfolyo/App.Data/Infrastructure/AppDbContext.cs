using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PortfolyoApp.Data.Entities.FileEntity;

namespace PortfolyoApp.Data.Infrastructure
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<FileEntity> Files { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileEntityConfiguration());
            //modelBuilder.Entity<UserEntity>().HasData(users);
            //modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}

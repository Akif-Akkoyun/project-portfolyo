using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Data.Entities;
using static PortfolyoApp.Data.Entities.RoleEntity;

namespace PortfolyoApp.Auth.Api.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<RoleEntity> Roles { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}

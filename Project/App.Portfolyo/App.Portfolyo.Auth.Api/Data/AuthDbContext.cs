using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Auth.Api.Data.Entites;
using static PortfolyoApp.Auth.Api.Data.Entites.RoleEntity;

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
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}

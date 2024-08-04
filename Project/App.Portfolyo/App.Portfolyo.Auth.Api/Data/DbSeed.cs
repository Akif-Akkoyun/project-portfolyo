using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Auth.Api.Data.Entites;

namespace PortfolyoApp.Auth.Api.Data
{
    public class DbSeed
    {
        public static async Task SeedAsync(DbContext context)
        {
            string password = BCrypt.Net.BCrypt.HashPassword("1234");
            var adminRole = new RoleEntity
            {
                Name = "Admin",
            };
            var commenterRole = new RoleEntity
            {
                Name = "Commenter",
            };
            context.Set<RoleEntity>().AddRange(adminRole, commenterRole);
            await context.SaveChangesAsync();
            List<UserEntity> users = new()
            {
                new UserEntity
                {
                    UserName = "Akif",
                    UserSurName = "Akkoyun",
                    Email = "akifakkoyun09@gmail.com",
                    PasswordHash = password,
                    CreatedAt = DateTime.Now,
                    RoleId = adminRole.Id,
                },
                new UserEntity
                {
                    UserName = "Jane",
                    UserSurName = "Doe",
                    Email = "jane@gmail.com",
                    PasswordHash = password,
                    RoleId = commenterRole.Id,
                    CreatedAt = DateTime.Now
                },
                new UserEntity
                {
                    UserName = "Alice",
                    UserSurName = "Doe",
                    Email = "alice@gmail.com",
                    PasswordHash = password,
                    RoleId = commenterRole.Id,
                    CreatedAt = DateTime.Now
                }
            };
            context.Set<UserEntity>().AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}

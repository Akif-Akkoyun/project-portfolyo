using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Data.Entities;

namespace PortfolyoApp.Auth.Api.Data
{
    public class DbSeed
    {
        public static async Task SeedAsync(DbContext context)
        {
            //string password = BCrypt.Net.BCrypt.HashPassword("1234");
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
                    PasswordHash = "1234",
                    CreatedAt = DateTime.UtcNow,
                    RoleId = 1,

                },
                new UserEntity
                {
                    UserName = "Jane",
                    UserSurName = "Doe",
                    Email = "jane@gmail.com",
                    PasswordHash = "1234",
                    RoleId = 2,
                    CreatedAt = DateTime.UtcNow
                },
                new UserEntity
                {
                    UserName = "Alice",
                    UserSurName = "Doe",
                    Email = "alice@gmail.com",
                    PasswordHash = "1234",
                    RoleId = 2,
                    CreatedAt = DateTime.UtcNow
                }
            };
            context.Set<UserEntity>().AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
